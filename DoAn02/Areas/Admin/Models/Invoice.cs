using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn02.Models
{
    public class Invoice
    {

        public int Id { get; set; }
        public string Code { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public DateTime IssuedDate { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPhone { get; set; }
        public int Total { get; set; }
        public bool Status { get; set; }
        public int StatusId { get; set; }
        public Product ProductItems { get; set; }
        [NotMapped]
        public List<SelectListItem> Trangthai { set; get; }
        [NotMapped]
        public List<InvoiceDetail> invoiceDetails { get; set; }
    }
}
