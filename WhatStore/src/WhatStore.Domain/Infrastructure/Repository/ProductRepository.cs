using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Crosscutting.Infrastructure.Contexts;
using WhatStore.Crosscutting.Infrastructure.Models.Product;

namespace WhatStore.Crosscutting.Infrastructure.Repository
{
    public class ProductRepository
    {
        private CustomSettings _settings;

        public async Task<bool> SetProduct()
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
