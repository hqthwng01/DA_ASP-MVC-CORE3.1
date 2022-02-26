using DoAn02.Areas.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn02.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [DisplayName("Tài khoản")]
        public string UserId { get; set; }
        // Navigation reference property cho khóa ngoại đến Account
        [ForeignKey("UserId")]
        [DisplayName("Tài khoản")]
        public ApplicationUser User { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
