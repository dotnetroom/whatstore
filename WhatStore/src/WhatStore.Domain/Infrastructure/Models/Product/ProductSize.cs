using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Domain.Infrastructure.Models.Product
{
    public class ProductSize
    {
        [Key]
        public string ProductId { get; set; }
        [Required]
        public int SizeId { get; set; } 

        public virtual Product Product { get; set; }
        
        public virtual Size Size  { get; set; }
    }
}
