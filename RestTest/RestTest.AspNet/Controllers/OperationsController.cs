using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestTest.AspNet.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly ILogger<OperationsController> _logger;

        public OperationsController(ILogger<OperationsController> logger)
        {
            _logger = logger;
        }

        [HttpPost("sum")]
        public IActionResult SumOperation([FromBody] TwoNumbersBody twoNumber)
        {
            return Ok(new { Result = twoNumber.num1 + twoNumber.num2 });
        }

        [HttpPost("sub")]
        public IActionResult SubOperation([FromBody] TwoNumbersBody twoNumber)
        {
            return Ok(new { Result = twoNumber.num1 + twoNumber.num2 });
        }

        [HttpGet("name")]
        public IActionResult GetName()
        {
            Request.Headers.TryGetValue("fullname", out var resp);
            return Ok(new { fullname = resp.ToString() });
        }
    }

    public class TwoNumbersBody
    {
        public long num1 { get; set; }
        public long num2 { get; set; }
    }
}
