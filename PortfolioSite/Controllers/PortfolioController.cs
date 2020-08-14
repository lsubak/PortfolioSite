using Microsoft.AspNetCore.Mvc;

namespace PortfolioSite.Controllers
{
    public class PortfolioController : Controller
    {
        [Route("Portfolio")]
        [Route("Portfolio/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
