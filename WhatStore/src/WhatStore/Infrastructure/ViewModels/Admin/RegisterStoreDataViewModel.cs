using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Infrastructure.ViewModels.Admin
{
    public class RegisterStoreDataViewModel
    {
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string PhoneDDD { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
        public ICollection<IFormFile> Pictures { get; set; }
        public string Terms { get; set; }
        public string Dress { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string CEP { get; set; }
        public string Complemento { get; set; }
        public int State { get; set; }
        public int City { get; set; }
    }

}
