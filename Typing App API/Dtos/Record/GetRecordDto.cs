
namespace Typing_App_API.Dtos.Record
{
    using Typing_App_API.Models;
    public class GetRecordDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Time { get; set; }
        public GetUserDtoSafe? User { get; set; }
    }
}
