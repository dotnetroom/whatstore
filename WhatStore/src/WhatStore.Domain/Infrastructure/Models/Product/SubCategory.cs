using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Domain.Infrastructure.Models.Product
{
    public class SubCategory
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string SubCategoryName { get; set; }

        public long CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
