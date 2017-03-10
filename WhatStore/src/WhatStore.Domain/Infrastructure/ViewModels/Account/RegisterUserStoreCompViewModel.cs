using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Localization;

namespace WhatStore.Domain.Infrastructure.ViewModels.Account
{
    public class RegisterUserStoreCompViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string RG { get; set; }

        [Required]
        public string CPF { get; set; }

        public string PhoneDDD { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        public bool Genero { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public int State { get; set; }

        [Required]
        public int CityID { get; set; }

        public string CityName { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Number { get; set; }

        public string Complement { get; set; }
        
        [Required]
        public List<State> States { get; set; }

        [Required]
        public long AdressId { get; set; }

        [Required]
        public int StateID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string NormalizedEmail { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string NormalizedUserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnMessage { get; set; }
    }
}
