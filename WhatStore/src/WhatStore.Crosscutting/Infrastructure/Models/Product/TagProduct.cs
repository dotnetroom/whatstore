﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Models.Product
{
    public class TagProduct
    {
        public string ProductId { get; set; }
        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual Product Product { get; set; }

    }
}
