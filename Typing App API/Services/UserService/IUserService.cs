namespace Typing_App_API.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAll();
        Task<ServiceResponse<GetUserDto>> GetSingle(int id);
        Task<ServiceResponse<UserCredsDto>> AddUser(AddUserDto newUser);
        Task<ServiceResponse<UserCredsDto>> LoginUser(AddUserDto loginRequest);
        Task<ServiceResponse<GetUserDto>> UpdateUser(int id, UpdateUserDto updatedUser);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);

    }
}
