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
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetSingle(int id)
        {
            return Ok(await _userService.GetSingle(id));
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> AddUser(AddUserDto newUser)
        {
            return Ok(await _userService.AddUser(newUser));
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUser(int id, UpdateUserDto updatedUser)
        {
            var response = await _userService.UpdateUser(id, updatedUser); 

            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
