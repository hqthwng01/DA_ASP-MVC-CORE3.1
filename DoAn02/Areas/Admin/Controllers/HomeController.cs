using DoAn02.Data;
using DoAn02.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn02.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly DoAnContext _context;

        public HomeController(DoAnContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> DashboardAsync()
        {
            ViewBag.HD = (from a in _context.Invoices
                          select a).Count();

            ViewBag.SP = (from b in _context.Products
                          select b).Count();

            ViewBag.LSP = (from b in _context.ProductTypes
                           select b).Count();

            ViewBag.TK = (from b in _context.Accounts
                          select b).Count();
            var doAnContext = _context.Invoices.Include(i => i.User);
            return View(await doAnContext.ToListAsync());
        }
    }
}
