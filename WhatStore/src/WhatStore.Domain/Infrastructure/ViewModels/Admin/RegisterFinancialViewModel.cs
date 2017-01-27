using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Localization;

namespace WhatStore.Domain.Infrastructure.ViewModels.Admin
{
    public class RegisterFinancialViewModel
    {
        public string About { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name field is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The CPF field is required.")]
        public string CPF { get; set; }

        public string Rg { get; set; }

        [Required(ErrorMessage = "The Birth Day field is required.")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "The Gender field is required.")]
        public bool Gender { get; set; }

        public string DDD { get; set; }
        public string Phone { get; set; }

        [Required]
        public bool IsPessoaJuridica { get; set; }

        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }

        [Required(ErrorMessage = "The CEP field is required.")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "The State field is required.")]
        public int State { get; set; }

        [Required(ErrorMessage = "The City field is required.")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "The Adress field is required.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "The Adress Number field is required.")]
        public string Number { get; set; }

        public string Complement { get; set; }

        public string ReturnMessage { get; set; }

        public List<State> States { get; set; }
    }
}
