using Microsoft.AspNetCore.Mvc;

namespace PortfolioSite.Controllers
{
    public class AboutController : Controller
    {
        [Route("About")]
        [Route("About/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
