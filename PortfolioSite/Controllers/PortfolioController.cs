﻿using Microsoft.AspNetCore.Mvc;

namespace PortfolioSite.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
