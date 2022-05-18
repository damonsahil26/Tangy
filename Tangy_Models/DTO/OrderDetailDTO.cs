﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Models.DTO
{
    public class OrderDetailDTO
    {
        public int OrderId { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Size { get; set; }
        [Required]
        public string ProductName { get; set; }
    }
}
