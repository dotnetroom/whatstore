using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WhatStore.Domain.Infrastructure.Repository.Interfaces
{
    public interface IProductRepository
    {

        Task<bool> UpdateProduct(long id, string productName, string description, double price, ICollection<IFormFile> picture, 
                                 bool hasVariety, string colors, string sizes, bool isFreeShip, double length, double weight, 
                                 double height, string tags);
    }
}
