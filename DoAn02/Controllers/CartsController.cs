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

namespace DoAn02.Controllers
{
    public class CartsController : Controller
    {
        private readonly DoAnContext _context;

        public CartsController(DoAnContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index(string username)
        {
            var doAnContext = _context.Carts.Include(c => c.Account).Include(c => c.Product);
            ViewBag.Total = _context.Carts.Sum(c => c.Quantity * c.Product.Price);
            return View(await doAnContext.ToListAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Account)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Password");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountId,ProductId,Quantity")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Password", cart.AccountId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cart.ProductId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Password", cart.AccountId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cart.ProductId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountId,ProductId,Quantity")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Password", cart.AccountId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cart.ProductId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Account)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add(int id)
        {
            return Add(id, 1);
        }
        [HttpPost]
        public IActionResult Add(int productId, int quantity)
        {
            string username = HttpContext.Session.GetString("AccountUsername");
            int accountId = _context.Accounts.FirstOrDefault(a => a.Username == username).Id;
            Cart cart = _context.Carts.FirstOrDefault(c => c.AccountId == accountId && c.ProductId == productId);           
            if (cart == null)
            {
                cart = new Cart();
                cart.AccountId = accountId;
                cart.ProductId = productId;
                cart.Quantity = quantity;
                _context.Carts.Add(cart);
            }
            else
            {
                cart.Quantity += quantity;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool Ktra(string username)
        {
            List<Cart> carts = _context.Carts.Include(c => c.Product).Include(c => c.Account)
                .Where(c => c.Account.Username == username).ToList();
            foreach (Cart c in carts)
            {
                if (c.Product.Stock < c.Quantity)
                {
                    return false;
                }
            }
            return true;
        }

        public IActionResult Pay()
        {
            string username = HttpContext.Session.GetString("AccountUsername");
            ViewBag.Account = _context.Accounts.Where(a => a.Username == username).FirstOrDefault();
            ViewBag.CartTotal = _context.Carts.Include(c => c.Product).Include(c => c.Account)
                .Where(c => c.Account.Username == username)
                .Sum(c => c.Quantity * c.Product.Price);
            return View();
        }

        [HttpPost]
        public IActionResult Pay([Bind("ShippingAddress,ShippingPhone")] Invoice invoice)
        {
            string username = HttpContext.Session.GetString("AccountUsername");

            if (!CheckStock(username))
            {
                ViewBag.ErrorMessage = "sản phẩm hết hàng. Vui lòng kiểm tra.";
                ViewBag.Account = _context.Accounts.Where(a => a.Username == username).FirstOrDefault();
                ViewBag.CartsTotal = _context.Carts.Include(c => c.Product).Include(c => c.Account)
                    .Where(c => c.Account.Username == username)
                    .Sum(c => c.Quantity * c.Product.Price);
                return View();
            }

            //thêm hóa đơn

            DateTime now = DateTime.Now;
            invoice.Code = now.ToString("yyMMddhhmmss");
            invoice.AccountId = _context.Accounts.FirstOrDefault(a => a.Username == username).Id;
            invoice.IssuedDate = now;
            invoice.Total = _context.Carts.Include(c => c.Product).Include(c => c.Account)
                .Where(c => c.Account.Username == username)
                .Sum(c => c.Quantity * c.Product.Price);

            _context.Add(invoice);
            _context.SaveChanges();

            // thêm chi tiết hóa đơn
            List<Cart> carts = _context.Carts.Include(c => c.Product).Include(c => c.Account)
                .Where(c => c.Account.Username == username).ToList();

            foreach (Cart c in carts)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail();
                invoiceDetail.InvoiceId = invoice.Id;
                invoiceDetail.ProductId = c.ProductId;
                invoiceDetail.Quantity = c.Quantity;
                invoiceDetail.UnitPrice = c.Product.Price;
                _context.Add(invoiceDetail);
            }

            _context.SaveChanges();

            //trừ sl tồn kho và xóa giỏ hàng
            foreach (Cart c in carts)
            {
                c.Product.Stock -= c.Quantity;
                _context.Carts.Remove(c);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        private bool CheckStock(string username)
        {
            List<Cart> carts = _context.Carts.Include(c => c.Product).Include(c => c.Account)
                .Where(c => c.Account.Username == username).ToList();

            foreach (Cart c in carts)
            {
                if (c.Product.Stock < c.Quantity)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
