namespace Typing_App_API.Dtos.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = "username";
        public string Password { get; set; } = "pasword";
    }
}
