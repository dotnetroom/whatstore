using System;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using WhatStore.Crosscutting.Infrastructure.Contexts;
using WhatStore.Crosscutting.Infrastructure.Repository.Interfaces;
using System.Data.SqlClient;

namespace WhatStore.Crosscutting.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private CustomSettings _settings;
        public ProductRepository(CustomSettings settings)
        {
            _settings = settings;
        }

        public async Task<bool> UpdateProduct(string nomeProduct, string description, double price, bool hasVariety, string colors, string size,
                                              bool isFreeShip, double weight, double height, double lenth, string tags)
        {
            var product = new Product()
            {
                NomeProduct = nomeProduct,
                Description = description,
                Price = price,
                HasVariety = hasVariety,
                Colors = colors,
                Size = size,
                IsFreeShip = isFreeShip,
                Weight = weight,
                Height = heigth,
                Lenth = lenth,
                Tags = tags
            };
        }
    }
}
