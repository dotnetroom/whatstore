using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Infrastructure.ViewModels.Admin
{
    public class RegisterUserViewModel
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public DateTime Birthday { get; set; }
        public bool Genero { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
