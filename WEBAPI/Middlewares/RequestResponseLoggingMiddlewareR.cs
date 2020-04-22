using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WEBAPI.Logger;

namespace WEBAPI.Middlewares
{
    public class RequestResponseLoggingMiddlewareR
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestResponseLoggingMiddlewareR(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;

            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "request-response.txt"));
            _logger = loggerFactory.CreateLogger("Req logger");

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {

                var request = context.Request;
                if (request.Path.StartsWithSegments(new PathString("/api")))
                {
                    var stopWatch = Stopwatch.StartNew();
                    var requestTime = DateTime.UtcNow;
                    var requestBodyContent = await ReadRequestBody(request);
                    var originalBodyStream = context.Response.Body;
                    using (var responseBody = new MemoryStream())
                    {
                        var response = context.Response;
                        response.Body = responseBody;
                        await _next(context);
                        stopWatch.Stop();

                        string responseBodyContent = null;
                        responseBodyContent = await ReadResponseBody(response);
                        await responseBody.CopyToAsync(originalBodyStream);
                        //requestTime,
                        //stopWatch.ElapsedMilliseconds,
                        //response.StatusCode,
                        //request.Method,
                        //request.Path,
                        //request.QueryString.ToString(),
                        //requestBodyContent,
                        //responseBodyContent
                        _logger.Log(LogLevel.Critical, requestBodyContent);

 
                    }
                }
                else
                {
                    await _next(context);
                }
            }
            catch (Exception ex)
            {
                await _next(context);
            }
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }
    }
}
