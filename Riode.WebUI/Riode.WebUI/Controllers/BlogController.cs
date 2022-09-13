﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Riode.WebUI.Controllers
{
    public class BlogController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Comment(int postId, string comment)
        {
            return View();
        }
    }
}
