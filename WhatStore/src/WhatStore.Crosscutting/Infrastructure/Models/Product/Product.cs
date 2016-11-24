using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Models.Product
{
    public class Product
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public int StoreID { get; set; }
        [Required]
        public string Name { get; set; }
        [Requerid]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public bool IsFreeShipping  { get; set; }
        public string Length { get; set; }
        public string Widith { get; set; }
        public string Weigth { get; set; }

    }
}
