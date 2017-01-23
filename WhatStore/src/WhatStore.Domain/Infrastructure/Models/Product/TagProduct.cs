using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Domain.Infrastructure.Models.Product
{
    public class TagProducts
    {
        public string ProductId { get; set; }
        public long TagId { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual Product Product { get; set; }

    }
}
