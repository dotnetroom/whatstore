using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Crosscutting.Infrastructure.Models.Product;

namespace WhatStore.Infrastructure.ViewModels.Admin
{
    public class RegisterProductViewModel
    {
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Color { get; set; }
        public List<ProductSize> Size { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Length { get; set; }
        public ICollection<IFormFile> Picture { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
