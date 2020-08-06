using Microsoft.AspNetCore.Mvc;
using PortfolioSite.Models;
using PortfolioSite.Models.Enums;

namespace PortfolioSite.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index(SiteTheme siteTheme)
        {
            return View(new SiteThemeModel(siteTheme));
        }
    }
}
