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
                                              bool isFreeShip, double weight, double height, double lenth, string tags)
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

                    return true;
                }
            }
        }
    }
}
