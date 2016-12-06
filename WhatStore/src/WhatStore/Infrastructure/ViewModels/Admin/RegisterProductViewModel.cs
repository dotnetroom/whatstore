using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Infrastructure.ViewModels.Admin
{
    public class RegisterProductViewModel
    {
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}
