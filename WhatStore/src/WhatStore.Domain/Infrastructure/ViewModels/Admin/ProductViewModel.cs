using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Product;

namespace WhatStore.Domain.Infrastructure.ViewModels.Admin
{
    public class ProductViewModel
    {

        public string Id { get; set; }
       
        public string Name { get; set; }

        public string Pictures { get; set; }

    } 
}
