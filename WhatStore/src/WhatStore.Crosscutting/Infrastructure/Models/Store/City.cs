﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace WhatStore.Crosscutting.Infrastructure.Models.Store
{
    public class City
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public int StateID { get; set; }
        [Required]
        public String Name { get; set; }

        public virtual State State { get; set; }
    }
}
