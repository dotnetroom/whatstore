using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Localization;

namespace WhatStore.Domain.Infrastructure.Models.Store
{
    public class Store
    {
        [Key]
        public long Id { get; set; }

        public long PessoaJuridicaId { get; set; }
                
        public long AdressId { get; set; }

        [Required]
        public int StoreTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string URL { get; set; }

        public string Logo { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string Term { get; set; }

        
        public virtual Adress Adress { get; set; }
        public virtual StoreType StoreType { get; set; }
        public virtual PessoaJuridica PessoaJuridica { get; set; }
    }
}
