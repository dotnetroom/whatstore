using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Models.Product
{
    public class Tag
    {
        [Key]
        public long TagId { get; set; }
        [Required]
        public string TagName { get; set; }
    }
}
