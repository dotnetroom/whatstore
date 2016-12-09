using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Repository.Interfaces
{
    public interface IProductRepository
    {

        Task<bool> UpdateProduct(long idStore, string nomeProduct, string description, double price, bool hasVariety, string colors, string size,
                                 bool isFreeShip, double weight, double height, double lenth, string tags);

    }
}
