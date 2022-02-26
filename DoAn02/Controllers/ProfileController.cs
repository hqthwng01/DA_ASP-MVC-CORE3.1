using AspNetCoreHero.ToastNotification.Abstractions;
using DoAn02.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn02.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DoAnContext _context;
        private readonly INotyfService _notyf;
        public ProfileController(DoAnContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }
        public async Task<IActionResult> ProfileSearch(int? id)
        {
            if (HttpContext.Session.Keys.Contains("AccountUsername"))
            {
                ViewBag.AccountUsername = HttpContext.Session.GetString("AccountUsername");
            }
            if (id == null)
            {
                return NotFound();
            }
            var acc = await _context.Accounts.FindAsync(id);

            if (acc == null)
            {
                return NotFound();
            }
            return View(acc);
        }
    }
}
