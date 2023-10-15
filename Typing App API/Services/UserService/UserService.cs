using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Typing_App_API.Models;

namespace Typing_App_API.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public UserService(
            IMapper mapper, 
            IConfiguration configuration, 
            IHttpContextAccessor httpContextAccessor,
            DataContext context       
            )
        {
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<ServiceResponse<string>> AddUser(AddUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<string>();

            try
            {
                // Hash password
                string passwordHash
                    = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

                newUser.Password = passwordHash;

                var newMappedUser = _mapper.Map<User>(newUser);
                _context.Users.Add(newMappedUser);
                await _context.SaveChangesAsync();

                var addedUser = await _context.Users.FindAsync(newMappedUser.Id);

                if (addedUser is null)
                {
                    throw new Exception("User not added to database successfully");
                }

                string token = CreateToken(addedUser);

                serviceResponse.Data = token;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> LoginUser(AddUserDto loginRequest)
        {
            var serviceResponse = new ServiceResponse<string>();  

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == loginRequest.Username);

                if (user is null)
                {
                    throw new Exception($"User with username {loginRequest.Username} not found");
                }            

                if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
                {
                    throw new Exception("Wrong password");
                }

                string token = CreateToken(user);

                serviceResponse.Data = token;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var dbUsers = await _context.Users.ToListAsync();
            serviceResponse.Data = dbUsers.Select(user => _mapper.Map<GetUserDto>(user)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> GetSingle(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user is null)
                {
                    throw new Exception($"User with id {id} not found");
                }

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(int id, UpdateUserDto updatedUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user is null)
                {
                    throw new Exception($"User with id {id} not found");
                }

                user.Username = updatedUser.Username;
                user.Password = updatedUser.Password;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();

            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user is null)
                {
                    throw new Exception($"User with id {id} not found");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                var updatedList = await _context.Users.ToListAsync();
                serviceResponse.Data = updatedList.Select(user => _mapper.Map<GetUserDto>(user)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }      

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> { 
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
