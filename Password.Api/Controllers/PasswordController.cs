using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Password.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly ILogger<PasswordController> _logger;

        public PasswordController(ILogger<PasswordController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<bool> Validate(string password)
        {
            _logger.LogInformation("Validate Starting");

            bool validate = Password.Validate(password, _logger);

            _logger.LogInformation("Validate Result: " + validate.ToString());

            return Ok(validate);
        }
    }
}
