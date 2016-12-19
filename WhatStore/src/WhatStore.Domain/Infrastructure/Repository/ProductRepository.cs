﻿using System;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WhatStore.Domain.Infrastructure.Contexts;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using WhatStore.Domain.Infrastructure.Models.Store;

namespace WhatStore.Domain.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private CustomSettings _settings;
        public ProductRepository(IOptions<CustomSettings> settings)
        {
            _settings = settings.Value;
        }

        public Task RegisterStore(Store store)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateProduct(long storeId, string productName, string description, double price, ICollection<IFormFile> picture,
                                              bool hasVariety, string colors, string sizes, bool isFreeShip, double length, double weight,
                                              double height, string tags)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                await db.OpenAsync();
                try
                {
                    var idStore = await db.QueryAsync<long>("SELECT StoreID FROM dbo.AspNetUsers");

                    if (idStore == null) return false;

                    var productInsert = "INSERT INTO dbo.\"Product\" (\"Name\", \"Description\", \"Price\", \"StoreId\") "
                                   + "VALUES (@NAME, @DESCRIPTION, @PRICE, @STOREID)";

                    var product = await db.ExecuteAsync(productInsert,
                        new
                        {
                            Name = productName,
                            Description = description,
                            Price = price,
                            StoreId = storeId
                        });

                    // if (hasVariety == true)
                    //{

                    //  var x = await db.ExecuteAsync(productInsert,
                    //    new
                    //  {
                    //    HasVariety = hasVariety,
                    //  Colors = colors,
                    //  Size = size
                    // }, trans);
                    //    }

                    if (isFreeShip == true)
                    {
                        var shippingInsert = "INSERT INTO dbo.\"Product\" (\"IsFreeShipping\", \"Lenth\", \"Width\" \"Weight\") "
                                                    + "VALUES (@IsFreeShipping, @Lenth, @Width, @Weight)";

                        var ship = await db.ExecuteAsync(shippingInsert,
                            new
                            {
                                IsFreeShip = isFreeShip,
                                Weight = weight,
                                Height = height,
                                Lenth = length,
                            });
                    }

                    var tagsInsert = "INSERT INTO dbo.\"Tag\" (\"TagName\") " + "VALUES (@TagName)";

                    var tag = await db.ExecuteAsync(tagsInsert,
                        new
                        {
                            Tags = tags
                        });
                    
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
