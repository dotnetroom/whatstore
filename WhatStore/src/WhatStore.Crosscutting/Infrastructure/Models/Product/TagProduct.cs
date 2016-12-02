using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Models.Product
{
    public class TagProduct
    {
        [Column(Order=0),Key]
        public string ProductId { get; set; }

        [Column(Order = 1), Key]
        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual Product Product { get; set; }

    }
}
