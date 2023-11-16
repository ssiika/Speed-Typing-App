namespace Typing_App_API.Services.TextService
{
    public interface ITextService
    {
        Task<ServiceResponse<List<Text>>> AddText(Text newText);

        Task<ServiceResponse<Text>> GetText();
    }
}
