using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace RestTest.AspNet.Controllers
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Id { get; private set; }

        private static volatile int _id;

        public User()
        {
            Id = NextId();
        }

        private int NextId()
        {
            Interlocked.Increment(ref _id);
            return _id;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserCrudController : ControllerBase
    {
        private static readonly ConcurrentDictionary<int, User> _db = new ConcurrentDictionary<int, User>();

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new { user = _db[id] });
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            _db[user.Id] = user;
            return Ok(new { user_id = user });
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _db.TryRemove(id, out var _);
        }
    }
}
