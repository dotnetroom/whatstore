using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Repository.Interfaces
{
    public interface IProductRepository
    {
<<<<<<< HEAD
        public async Task<bool> SetProduct(string nomeProduct, string description, double price, bool hasVariety, string colors, string size, bool isFreeShip,
                              double weight, double height, double lenth, string tags)
        {
        }
=======
        Task<bool> UpdateProduct(string nomeProduct, string description, double price, bool hasVariety, string colors, string size,
                                 bool isFreeShip, double weight, double height, double lenth, string tags);
>>>>>>> origin/1.0
    }
}
