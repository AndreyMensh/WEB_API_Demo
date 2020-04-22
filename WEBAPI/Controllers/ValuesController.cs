using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WEBAPI.Logger;
using WEBAPI.Services.Contracts;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ISmsService _smsService;
        private readonly ILogger _logger;

        public ValuesController(ISmsService smsService, ILoggerFactory loggerFactory)
        {

            _smsService = smsService;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            //_smsService.SendSms(new SendSmsViewModel
            //{
            //    Message = "Здарова",
            //    PhoneNumber = "+375298061823"
            //});



            var remoteIp = ControllerContext.HttpContext.Connection.RemoteIpAddress.ToString();    
            return Ok(remoteIp);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
