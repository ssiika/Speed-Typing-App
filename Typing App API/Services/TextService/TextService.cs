using Microsoft.EntityFrameworkCore;
using Typing_App_API.Models;

namespace Typing_App_API.Services.TextService
{
    public class TextService : ITextService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public TextService(
            IMapper mapper,
            DataContext context
            )
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetTextDto>>> AddText(AddTextDto newText)
        {
            var serviceResponse = new ServiceResponse<List<GetTextDto>>();

            try
            {
                var mappedText = _mapper.Map<Text>(newText);

                _context.Text.Add(mappedText);
                await _context.SaveChangesAsync();

                // Return list of all texts to confirm newText was added correctly 

                var dbTexts = await _context.Text.ToListAsync();
                serviceResponse.Data = dbTexts.Select(text => _mapper.Map<GetTextDto>(text)).ToList();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTextDto>> GetText()
        {
            throw new NotImplementedException();
        }
    }
}
