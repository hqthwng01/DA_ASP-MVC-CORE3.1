using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAn02.Data;
using DoAn02.Models;
using Microsoft.AspNetCore.Http;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace DoAn02.Controllers
{
    public class AccountController : Controller
    {
        private readonly DoAnContext _context;
        private readonly INotyfService _notyf;

        public AccountController(DoAnContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        public IActionResult Signup()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("Id,Username,Password,Email,FullName")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Home");
            }
            return View(account);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Profile(int? id)
        {
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

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(int id, [Bind("Id,Username,Email,Phone,Address,FullName")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            Account acc = _context.Accounts.Where(a => a.Username == Username && a.Password == Password).FirstOrDefault();
            if (acc != null)
            {
                if (acc.Status == true)
                {
                    HttpContext.Session.SetInt32("AccountID", acc.Id);
                    HttpContext.Session.SetString("AccountUsername", acc.Username);
                    HttpContext.Session.SetString("Email", acc.Email);
                    _notyf.Success("Đăng nhập thành công", 3);
                    return RedirectToAction("Index", "Home");  
                }
                else
                {
                    ViewBag.Error = "Lỗi! Tài khoản đã bị khóa";
                    return View();
                }
            }
            else
            {
                ViewBag.Msg = "Lỗi! Sai username hoặc mật khẩu";
                return View();
            }
        }
        
        public IActionResult Logout()
        {
            /*
            HttpContext.Response.Cookies.Append("AccountID", "", new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
            HttpContext.Response.Cookies.Append("AccountUsername", "", new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
            */
            //HUY 1 THANH PHAN TRONG SESSION
            // HttpContext.Session.Remove("AccountID");
            //HUY TOAN BO SESSION
            HttpContext.Session.Clear();
            _notyf.Custom("Bạn đã đăng xuất", 3, "whitesmoke", "bx bx-log-out");
            return RedirectToAction("Index", "Home");
        }
        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
