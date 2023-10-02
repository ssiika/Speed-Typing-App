using System.Text.Json.Serialization;

namespace Typing_App_API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Length
    {
        Short = 1,
        Medium = 2,
        Long = 3,
    }
}
