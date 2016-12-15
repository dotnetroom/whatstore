using System.ComponentModel.DataAnnotations;

namespace WhatStore.Domain.Infrastructure.Models.Product
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Language { get; set; }

    }
}