namespace Typing_App_API.Services.UserService
{
    public class UserService : IUserService
    {

        private static List<User> testUsers = new List<User> {
            new User(),
            new User {Id=1, Username="Testo"}
        };

        public async Task<List<User>> AddUser(User newUser)
        {
            testUsers.Add(newUser);
            return testUsers;
        }

        public async Task<List<User>> GetAll()
        {
            return testUsers;
        }

        public async Task<User> GetSingle(int id)
        {
            var user = testUsers.FirstOrDefault(user => user.Id == id);
            if (user is not null)
            {
                return user;
            }
            throw new Exception("User not found");
            
        }
    }
}
