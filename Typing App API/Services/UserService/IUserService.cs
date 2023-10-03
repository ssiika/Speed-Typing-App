namespace Typing_App_API.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<User>>> GetAll();
        Task<ServiceResponse<User>> GetSingle(int id);
        Task<ServiceResponse<List<User>>> AddUser(User newUser);

    }
}
