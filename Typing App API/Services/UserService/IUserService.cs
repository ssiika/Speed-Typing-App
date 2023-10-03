namespace Typing_App_API.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetSingle(int id);
        Task<List<User>> AddUser(User newUser);

    }
}
