using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn02.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "Gmail không hợp lệ")]
        public string Email { get; set; }
        [RegularExpression("0\\d{9}", ErrorMessage = "SĐT không hợp lệ")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }
        public string Avatar { get; set; }
        [NotMapped]
        public IFormFile AvtFile { get; set; }
        public bool Status { get; set; }
        public List<Invoice> Invoices { get; set; }

    }
}
