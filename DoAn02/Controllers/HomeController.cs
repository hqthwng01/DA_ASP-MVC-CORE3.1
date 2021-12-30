using DoAn02.Data;
using DoAn02.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn02.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {

            _logger = logger;
        }

        public IActionResult Index()
        {
            //hien thi cookies
            /*
            if (HttpContext.Request.Cookies.ContainsKey("AccountUserName"))
            {
                ViewBag.AccountUserName = HttpContext.Request.Cookies["AccountUsername"].ToString();
            }
            */
            if (HttpContext.Session.Keys.Contains("AccountUsername"))
            {
                ViewBag.AccountUsername = HttpContext.Session.GetString("AccountUsername");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
