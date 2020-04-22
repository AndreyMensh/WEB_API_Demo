using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;

namespace WEBAPI.Controllers
{
    [Route("api/errors")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        private readonly ApplicationDatabaseContext _context;

        public ErrorsController(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Errors
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Errors.ToList());
        }

        // POST: api/Errors
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]CreateErrorViewModel errorMessage)
        {
            var error = new Error
            {
                Message = errorMessage.Message,
                Date = DateTime.UtcNow
            };
            _context.Errors.Add(error);
            _context.SaveChanges();

            return Ok();
        }

    }

    public class CreateErrorViewModel
    {
        public string Message { get; set; }
    }
}
