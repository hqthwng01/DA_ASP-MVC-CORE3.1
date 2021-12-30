﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn02.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Account Account { get; set; }
        public Product Product { get; set; }
    }
}