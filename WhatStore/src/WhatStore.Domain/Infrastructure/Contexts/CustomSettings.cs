using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Domain.Infrastructure.Contexts
{
    public class CustomSettings
    {
        public string ConnectionString { get; set; }
        public string ImagePath { get; set; }
    }
}
