using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Domain.Infrastructure.Models.Localization
{
    public class Adress
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public int CityID { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Complement { get; set; }

        public virtual City City { get; set; }
    }
}
