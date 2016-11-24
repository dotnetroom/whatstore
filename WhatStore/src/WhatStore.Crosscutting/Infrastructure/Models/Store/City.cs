using System;
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
        public int Id { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual State State { get; set; }
    }
}
