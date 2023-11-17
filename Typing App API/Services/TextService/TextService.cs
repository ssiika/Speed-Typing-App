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

        public ServiceResponse<GetTextDto> GetText()
        {
            var serviceResponse = new ServiceResponse<GetTextDto>();

            try
            {
                // Generate random integer between 0 and database length
                int total = _context.Text.Count();
                Random r = new();
                int offset = r.Next(0, total);

                var randomText = _context.Text.Skip(offset).FirstOrDefault() ??
                    throw new Exception("Could not find text");

                serviceResponse.Data = _mapper.Map<GetTextDto>(randomText);
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
