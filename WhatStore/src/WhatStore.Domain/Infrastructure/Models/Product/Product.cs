using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Domain.Infrastructure.Models.Product
{
    public class Product
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public int StoreID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public bool IsFreeShipping { get; set; }
        public string Length { get; set; }
        public string Widith { get; set; }
        public string Weigth { get; set; }
        [Required]
        public long SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

    }
}
