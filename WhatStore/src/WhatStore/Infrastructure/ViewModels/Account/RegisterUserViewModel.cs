using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Store;

namespace WhatStore.Infrastructure.ViewModels.Admin
{
    public class RegisterUserViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string RG { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        public bool Genero { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public string StoreName { get; set; }

        //[Required]
        //public List<StoreTypeVeiwModel> StoreType { get; set; }
    }
}
