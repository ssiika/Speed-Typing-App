using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Typing_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> testUsers = new List<User> {
            new User(),
            new User {Id=1, Username="Testo"}      
        };
        // GET: api/users
        [HttpGet("getall")]
        public ActionResult<List<User>> GetAll()
        {
            return Ok(testUsers);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetSingle(int id)
        {
            return Ok(testUsers.FirstOrDefault(user => user.Id == id));
        }

        // POST api/users
        [HttpPost]
        public ActionResult<List<User>> AddUser(User newUser)
        {
            testUsers.Add(newUser);
            return Ok(testUsers);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
