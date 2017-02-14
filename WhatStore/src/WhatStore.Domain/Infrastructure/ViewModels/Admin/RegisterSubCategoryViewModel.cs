using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Product;

namespace WhatStore.Domain.Infrastructure.ViewModels.Admin
{
    public class RegisterSubCategoryViewModel
    {
        public List<SubCategory> Subcategorys { get; set; }
        public string SubcategoryName { get; set; }
        public string ReturnMessage { get; set; }
    }
}
