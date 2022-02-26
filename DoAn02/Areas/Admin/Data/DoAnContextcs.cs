using DoAn02.Areas.Data;
using DoAn02.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn02.Data
{
    public class DoAnContext : IdentityDbContext<ApplicationUser>
    {
        public DoAnContext(DbContextOptions<DoAnContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //builder.Entity<Cart>(b =>
            //{
            //    b.HasKey(p => p.UserId);

            //    b.HasMany(p => p.ApplicationUsers)
            //    .WithOne(p => p.Carts);
            //});
            //builder.Entity<Invoice>(b =>
            //{
            //    b.HasKey(i => i.UserId);

            //    b.HasMany(i => i.ApplicationUsers)
            //    .WithOne(i => i.Invoices);
            //});

        }
        public  DbSet<Account> Accounts { get; set; }
        public  DbSet<Invoice> Invoices { get; set; }
        public  DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public  DbSet<Cart> Carts { get; set; }
        public  DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
