using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Models.Store
{
    public class State
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
    }
}
