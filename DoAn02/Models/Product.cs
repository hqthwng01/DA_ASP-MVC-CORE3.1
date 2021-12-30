using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn02.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Des { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public int ProductTypeId { get; set; }
        public ProductTypes ProductTypes { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public bool Status { get; set; }
        public List<Cart> Carts { get; set; }
        public List<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
