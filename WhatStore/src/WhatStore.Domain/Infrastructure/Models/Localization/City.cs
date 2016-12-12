using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatStore.Domain.Infrastructure.Models.Localization
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual State State { get; set; }
    }
}
