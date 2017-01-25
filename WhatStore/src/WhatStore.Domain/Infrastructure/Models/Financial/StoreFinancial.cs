using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Localization;
using WhatStore.Domain.Infrastructure.Models.Store;

namespace WhatStore.Domain.Infrastructure.Models.Financial
{
    public class StoreFinancial
    {
        [Key]
        public long Id { get; set; }

        public string About { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string CPF { get; set; }

        public string Rg { get; set; }

        public DateTime Birthday { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        public string Phone { get; set; }

        public long PessoaJuridicaId { get; set; }

        [Required]
        public long AdressId { get; set; }

        [Required]
        public long StoreId { get; set; }

        public virtual Adress Adress { get; set; }
        public virtual Store.Store Store { get; set; }
        public virtual PessoaJuridica PessoaJuridica { get; set; }

    }
}
