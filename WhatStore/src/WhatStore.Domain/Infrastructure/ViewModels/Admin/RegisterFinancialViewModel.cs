using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Domain.Infrastructure
{
    public class RegisterFinancialViewModel
    {
        public string Responsible { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Last Name field is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The CPF field is required.")]
        public string CPF { get; set; }

        public string RG { get; set; }

        [Required(ErrorMessage = "The Birth Day field is required.")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "The Gender field is required.")]
        public bool Gender { get; set; }

        public string DDD { get; set; }
        public string Phone { get; set; }
        public string CNPJ { get; set; }
        public string SocialName { get; set; }
        public string StateIncentive { get; set; }
        public string MunicipalRegistration { get; set; }

        [Required(ErrorMessage = "The CEP field is required.")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "The State field is required.")]
        public int State { get; set; }

        [Required(ErrorMessage = "The City field is required.")]
        public int City { get; set; }

        [Required(ErrorMessage = "The Adress field is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The Adress Number field is required.")]
        public string Number { get; set; }

        public string Complement { get; set; }
    }
}
