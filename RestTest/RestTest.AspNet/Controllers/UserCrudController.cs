using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;

namespace RestTest.AspNet.Controllers
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserCrudController : ControllerBase
    {
        private static ConcurrentDictionary<Guid, User> _db = new ConcurrentDictionary<Guid, User>();

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(new { user = _db[id] });
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            var id = Guid.NewGuid();
            _db[id] = user;
            return Ok(new { user_id = user });
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _db.TryRemove(id, out var _);
        }
    }
}
