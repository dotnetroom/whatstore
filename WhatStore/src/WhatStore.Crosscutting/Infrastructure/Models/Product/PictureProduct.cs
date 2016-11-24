using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Models.Product
{
    public class PictureProduct
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string MyProperty { get; set; }
        [Required]
        public string ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
