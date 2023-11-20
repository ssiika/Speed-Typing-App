using Microsoft.AspNetCore.Mvc;

namespace Typing_App_API.Controllers
{
    public class FrontEnd : Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return File("~/index.html", "text/html");
        }
    }
}
