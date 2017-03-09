using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Product;

namespace WhatStore.Domain.Infrastructure.ViewModels.Store
{
    public class CategoryViewModel
    {
        public long Id { get; set; }
        public Category category { get; set; }
        public List<SubCategory> subcategories { get; set; }
    }
}
