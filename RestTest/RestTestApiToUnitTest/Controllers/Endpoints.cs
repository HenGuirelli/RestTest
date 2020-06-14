using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace RestTestApiToUnitTest.Controllers
{
    [ApiController]
    [Route("api")]
    public class Endpoints : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Console.WriteLine("chamou endpoint");
            return Ok(new { Responsta1 = 10, resposta2 = "ola", resposta3 = new List<string> { "s", "b", "c"} });
        }
    }
}
