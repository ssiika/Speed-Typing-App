namespace Typing_App_API.Models
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Time { get; set; }
        public User? User { get; set; }
    }
}
