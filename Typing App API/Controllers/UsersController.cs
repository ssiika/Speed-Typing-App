using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Typing_App_API.Services.UserService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Typing_App_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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
            var response = await _userService.GetAll();

            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // GET api/users/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetSingle(int id)
        {
            var response = await _userService.GetSingle(id);

            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST api/users
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> AddUser(AddUserDto newUser)
        {
            
                
            return Ok(await _userService.AddUser(newUser));
        }

        // POST api/users/login
        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<string>>> LoginUser(AddUserDto loginRequest)
        {
            var response = await _userService.LoginUser(loginRequest);

            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
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


        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);

            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
