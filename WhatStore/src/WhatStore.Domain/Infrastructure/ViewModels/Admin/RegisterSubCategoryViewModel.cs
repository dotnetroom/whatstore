using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Product;

namespace WhatStore.Domain.Infrastructure.ViewModels.Admin
{
    public class RegisterSubCategory
    {
        public List<SubCategory> dubCategorys { get; set; }
        public string subCategorys { get; set; }
    }
}
