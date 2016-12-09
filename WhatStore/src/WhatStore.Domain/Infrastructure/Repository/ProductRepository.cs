using System;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using WhatStore.Crosscutting.Infrastructure.Repository.Interfaces;
using System.Data.SqlClient;
using WhatStore.Domain.Infrastructure.Contexts;

namespace WhatStore.Crosscutting.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private CustomSettings _settings;
        public ProductRepository(CustomSettings settings)
        {
            _settings = settings;
        }

        public async Task<bool> UpdateProduct(long idStore, string nomeProduct, string description, double price, bool hasVariety, string colors, string size,
                                              bool isFreeShip, double weight, double altura, double lenth, string tags)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                await db.OpenAsync();
                using (var trans = db.BeginTransaction())
                {
                    var result = await db.QueryAsync<long>("SELECT dbo.\"Product\".\"StoreID\" FROM dbo.\"Store\", dbo.\"Product\" WHERE" +
                            " dbo.\"Product\".\"StoreID\" = dbo.\"Store\".\"ID\" AND dbo.\"Product\".\"ID\" = @idStore", new { idStore = idStore });

                    var storeID = result.FirstOrDefault();

                    if (storeID <= 0) return false;

                    var productInsert = "INSERT INTO dbo.\"Product\" (\"Name\", \"Description\", \"Price\""
                                                        + "VALUES (@NAME, @DESCRIPTION, @PRICE)";

                    var product = await db.ExecuteAsync(productInsert,
                        new
                        {
                            NomeProduct = nomeProduct,
                            Description = description,
                            Price = price,
                        });

                    if (hasVariety == true)
                    {
                        var x = await db.ExecuteAsync(productInsert,
                            new
                            {
                                HasVariety = hasVariety,
                                Colors = colors,
                                Size = size                                
                            });
                    }

                    if (isFreeShip == true)
                    {
                        var ship = "INSERT INTO ";
                        var y = await db.ExecuteAsync(productInsert,
                            new
                            {
                                IsFreeShip = isFreeShip,
                                Weight = weight,
                                Height = altura,
                                Lenth = lenth,
                                Tags = tags
                            });
                    }

                        return true;
                    }
                }
            }
        }
    }
