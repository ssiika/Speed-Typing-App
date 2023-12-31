﻿namespace Typing_App_API.Services.TextService
{
    public interface ITextService
    {
        Task<ServiceResponse<List<GetTextDto>>> AddText(AddTextDto newText);

        Task<ServiceResponse<GetTextDto>> GetText();
    }
}
