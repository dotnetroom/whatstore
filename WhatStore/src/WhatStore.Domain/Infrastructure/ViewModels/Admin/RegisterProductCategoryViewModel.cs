﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Product;

namespace WhatStore.Domain.Infrastructure.ViewModels.Admin
{
    public class RegisterProductCategoryViewModel
    {
        public List<Category> Categories { get; set; }
        public string CategoryName { get; set; }
        public long CategoryId { get; set; }

        public List<SubCategory> Subcategories { get; set; }
        public string SubCategoryName { get; set; }
        public long SubCategoryId { get; set; }

        public long Category { get; set; }


        public string ReturnMessage { get; set; }
    }
}
