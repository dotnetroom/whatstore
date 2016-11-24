using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Models.Product;

namespace WhatStore.Crosscutting.Infrastructure.Models.Product
{
    public class ProuctSize
    {
        [Required]
        public string ProducId { get; set; }
        [Required]
        public int SizeId { get; set; }

        public virtual Product Product { get; set;}
        public virtual Size Size  { get; set; }
    }
}
