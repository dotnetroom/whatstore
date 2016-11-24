using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Models.Store
{
    public class Adress
    {
        [Key]
        [Required]
        public long ID { get; set; }
        [Required]
        public int CityID { get; set; }
        [Required]
        public String CEP { get; set; }
        [Required]
        public String Number { get; set; }
        [Required]
        public String Street { get; set; }
        [Required]
        public String Complement { get; set; }

        public virtual City City { get; set; }
    }
}
