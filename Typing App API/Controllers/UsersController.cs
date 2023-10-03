using Microsoft.AspNetCore.Mvc;
using Typing_App_API.Services.UserService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Typing_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet("getall")]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetSingle(int id)
        {
            return Ok(await _userService.GetSingle(id));
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User newUser)
        {
            return Ok(await _userService.AddUser(newUser));
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
