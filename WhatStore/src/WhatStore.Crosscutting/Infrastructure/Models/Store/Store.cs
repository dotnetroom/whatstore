using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Models.Store
{
    public class Store
    {
        [Key]
        [Required]
        public long ID { get; set; }
        [Required]
        public long PessoaJuridicaID { get; set; }
        [Required]
        public long AdressID { get; set; }
        [Required]
        public int StoreTypeID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }
        public String Phone { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String URL { get; set; }
        [Required]
        public String Logo { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public String Term { get; set; }

        public virtual Adress Adress { get; set; }
        public virtual StoreType StoreType { get; set; }
        public virtual PessoaJuridica PessoaJuridica { get; set; }
    }
}
