using DoAn02.Models;
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
    public class DoAnContext : DbContext
    {
        public DoAnContext(DbContextOptions<DoAnContext> options) : base(options)
        {
            
        }
        public  DbSet<Account> Accounts { get; set; }
        public  DbSet<Invoice> Invoices { get; set; }
        public  DbSet<ProductTypes> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public  DbSet<Cart> Carts { get; set; }
        public  DbSet<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
