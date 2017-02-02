using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Localization;

namespace WhatStore.Domain.Infrastructure.ViewModels.Admin
{
    public class RegisterStoreDataViewModel
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string StoreName { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string StoreDescription { get; set; }

        public string PhoneDDD { get; set; }

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string URL { get; set; }

        public ICollection<IFormFile> Pictures { get; set; }

        public string LogoName { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Terms { get; set; }

        [Required]
        public bool HasAdress { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string CEP { get; set; }

        public string Complemento { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public string CityName { get; set; }

        public List<State> States { get; set; }

        public string ReturnMessage { get; set; }

    }
}
