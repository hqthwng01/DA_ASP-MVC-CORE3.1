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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace DoAn02.Controllers
{
    public class AccountsController : Controller
    {
        private readonly DoAnContext _context;
        private readonly IWebHostEnvironment _his;

        public AccountsController(DoAnContext context, IWebHostEnvironment his)
        {
            _context = context;
            _his = his;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Accounts.ToListAsync());
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            Account acc = _context.Accounts.Where(a => a.Username == Username && a.Password == Password).FirstOrDefault();
            if (acc != null)
            {

                HttpContext.Session.SetInt32("AccountID", acc.Id);
                HttpContext.Session.SetString("AccountUsername", acc.Username);    
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.BaoLoi = "Dang nhap that bai";
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
            //HUY TOAN BO SESSION
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Email,Phone,Address,FullName,IsAdmin,Avatar,AnhFile,Status")] Account account, IFormFile AnhFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                if (account.AnhFile != null)
                {
                    var filename = account.Id.ToString() + Path.GetExtension(account.AnhFile.FileName);
                    var avtpath = Path.Combine(_his.WebRootPath, "avt");
                    var filepath = Path.Combine(avtpath, filename);
                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        account.AnhFile.CopyTo(fs);
                        fs.Flush();
                    }
                    account.Avatar = filename;
                    _context.Accounts.Update(account);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Email,Phone,Address,FullName,IsAdmin,Avatar,AnhFile,Status")] Account account, IFormFile AvatarFile)
        {
            if (id != account.Id)
            {
                return NotFound();
            }
            if (account.AnhFile != null)
            {
                var filename = account.Id.ToString() + Path.GetExtension(account.AnhFile.FileName);
                var avtpath = Path.Combine(_his.WebRootPath, "avt");
                var filepath = Path.Combine(avtpath, filename);
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    account.AnhFile.CopyTo(fs);
                    fs.Flush();
                }
                account.Avatar = filename;
                _context.Entry(account).State = EntityState.Modified;
                await Task.Delay(2000);
                await _context.SaveChangesAsync();
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
