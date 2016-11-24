using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WhatStore.Crosscutting.Infrastructure.Models.Identity
{
    public class ApplicationUser : IdentityUser<long>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string RG { get; set; }
        [Required]
        public bool Genero { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
    }
}
