using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WhatStore.Domain.Infrastructure.ViewModels.Admin
{
    public class RegisterProductViewModel
    {
        [Required(ErrorMessage = "The Product Name field is required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "The Variety field is required.")]
        public bool HasVariety { get; set; }

        public string Colors { get; set; }

        public string Sizes { get; set; }

        [Required(ErrorMessage = "The Ship field is required.")]
        public bool IsFreeShip { get; set; }

        public double Weight { get; set; } = 0;

        public double Height { get; set; } = 0;

        public double Length { get; set; } = 0;

        public ICollection<IFormFile> Picture { get; set; }

        [Required(ErrorMessage = "The Tags field is required.")]
        public string Tags { get; set; }

        public string ReturnMessage { get; set; }
    }
}
