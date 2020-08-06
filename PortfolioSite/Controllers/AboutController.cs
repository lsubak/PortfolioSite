using Microsoft.AspNetCore.Mvc;

namespace PortfolioSite.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
