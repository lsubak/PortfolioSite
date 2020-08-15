using Microsoft.AspNetCore.Mvc;

namespace PortfolioSite.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
