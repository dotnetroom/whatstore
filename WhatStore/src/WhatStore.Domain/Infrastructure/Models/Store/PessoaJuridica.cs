using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Models.Store
{
    public class PessoaJuridica
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string CNPJ { get; set; }
        [Required]
        public string InscricaoEstadual { get; set; }
        [Required]
        public string InscricaoMunicipal { get; set; }
        [Required]
        public string RazaoSocial { get; set; }
    }
}
