
namespace Typing_App_API.Dtos.Record
{
    using Typing_App_API.Models;
    public class GetRecordDto
    {
        public int Id { get; set; }
        public Length Length { get; set; }
        public int Time { get; set; }
        public User? User { get; set; }
    }
}
