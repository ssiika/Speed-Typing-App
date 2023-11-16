using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Typing_App_API.Services.TextService;

namespace Typing_App_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly ITextService _textService;

        public TextController(ITextService textService)
        {
            _textService = textService;
        }

        // POST: api/text
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetTextDto>>>> AddText(AddTextDto newText)
        {
            var response = await _textService.AddText(newText);

            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // GET: api/text
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<GetTextDto>>> GetText()
        {
            var response = await _textService.GetText();

            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
