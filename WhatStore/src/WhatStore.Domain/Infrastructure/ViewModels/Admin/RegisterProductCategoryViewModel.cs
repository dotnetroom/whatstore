using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Product;

namespace WhatStore.Domain.Infrastructure.ViewModels.Admin
{
    public class RegisterProductCategoryViewModel
    {
        public List<Category> Categorys { get; set; }
        public string ProductCategory { get; set; }
        public string ReturnMessage { get; set; }
    }
}
