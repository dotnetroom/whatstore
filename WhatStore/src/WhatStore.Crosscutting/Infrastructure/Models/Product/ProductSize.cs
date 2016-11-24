using System.ComponentModel.DataAnnotations;

namespace WhatStore.Crosscutting.Infrastructure.Models.Product
{
    public class ProductSize
    {
        [Required]
        public string ProductId { get; set; }
        [Required]
        public int SizeId { get; set; }

        public virtual Product Product { get; set;}
        public virtual Size Size  { get; set; }
    }
}
