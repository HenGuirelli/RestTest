using Microsoft.AspNetCore.Mvc;

namespace RestTest.AspNet.Controllers
{
    [ApiController]
    public class OperationsController : ControllerBase
    {
        [HttpPost("sum")]
        public IActionResult SumOperation([FromBody] TwoNumbersBody twoNumber)
        {
            return Ok(new { Result = twoNumber.num1 + twoNumber.num2 });
        }

        [HttpPost("sub")]
        public IActionResult SubOperation([FromBody] TwoNumbersBody twoNumber)
        {
            return Ok(new { Result = twoNumber.num1 - twoNumber.num2 });
        }

        [HttpGet("name")]
        public IActionResult GetName()
        {
            Request.Headers.TryGetValue("fullname", out var resp);
            Response.Headers.Add("fullname", resp.ToString());
            return Ok();
        }

        [HttpGet("infos")]
        public IActionResult ReceivedQueryString([FromQuery] string name, [FromQuery] long? age)
        {
            if (string.IsNullOrWhiteSpace(name) || age is null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("cookies")]
        public IActionResult ReceivedCookie()
        {
            if (Request.Cookies.ContainsKey("user-id"))
            {
                Response.Cookies.Append("logged", "true");
                return Ok();
            }
            return BadRequest();
        }
    }

    public class TwoNumbersBody
    {
        public long num1 { get; set; }
        public long num2 { get; set; }
    }
}
