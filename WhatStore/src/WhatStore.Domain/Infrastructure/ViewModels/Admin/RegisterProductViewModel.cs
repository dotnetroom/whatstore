using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Product;

namespace WhatStore.Domain.Infrastructure.ViewModels.Admin
{
    public class RegisterProductViewModel
    {
        [Required]
        public long StoreId { get; set; }

        [Required]
        public string Id { get; set; } 

        [Required(ErrorMessage = "The Product Name field is required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        public int Installments { get; set; }

        [Required(ErrorMessage = "The Variety field is required.")]
        public bool HasVariety { get; set; }

        public string Colors { get; set; }

        public string Sizes { get; set; }

        [Required(ErrorMessage = "The Ship field is required.")]
        public bool IsFreeShip { get; set; }

        public double Weigth { get; set; }

        public double Widith { get; set; }

        public double Length { get; set; }

        public ICollection<IFormFile> Picture { get; set; }

        public List<PictureProductViewModel> ImageName { get; set; }

        [Required(ErrorMessage = "The Tags field is required.")]
        public string Tags { get; set; }

        public List<Category> Categories { get; set; }

        public long Category { get; set; }

        public List<SubCategory> SubCategories { get; set; }

        public long SubCategory { get; set; }

        public string SubCategoryName { get; set; }

        public string ReturnMessage { get; set; }
    }
}
