﻿#region using System/Microsoft
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace PhotoHub.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        [HttpGet, Route("admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Stats()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }
    }
}