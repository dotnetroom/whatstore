using System;
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
using WhatStore.Domain.Infrastructure.Models.Product;

namespace WhatStore.Domain.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private CustomSettings _settings;
        public ProductRepository(IOptions<CustomSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<List<Product>> GetProducts(long storeID)
        {
            try
            {
                using (var db = new SqlConnection(_settings.ConnectionString))
                {
                    var result = await db.QueryAsync<Product>("SELECT * FROM \"dbo\".\"Product\" WHERE dbo.Product.StoreID = @STOREID",
                        new { STOREID = storeID }
                        );

                    return result.ToList();
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdateProduct(long storeId, string productName, string description, double price, ICollection<IFormFile> picture,
                                              bool hasVariety, string colors, string sizes, bool isFreeShip, double length, double weigth,
                                              double widtih, string tags, string id)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                await db.OpenAsync();
                try
                {
                    var idStore = await db.QueryAsync<long>("SELECT StoreID FROM dbo.AspNetUsers");

                    if (idStore == null) return false;

                    var codigo = id.Replace(" ", "");

                    var productInsert = "INSERT INTO dbo.\"Product\" (\"Id\",\"Name\", \"Description\", \"Price\", \"StoreId\", \"IsFreeShipping\") "
                                   + "VALUES (@ID, @NAME, @DESCRIPTION, @PRICE, @STOREID, @ISFREESHIPPING)";

                    var product = await db.ExecuteAsync(productInsert,
                        new
                        {
                            Id = codigo,
                            Name = productName,
                            Description = description,
                            Price = price,
                            StoreId = idStore,
                            IsFreeShipping = isFreeShip
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
                       

                        var shippingUpdate = "UPDATE dbo.Product SET Length = @Length, Widith = @Widith, Weigth = @Weigth WHERE Id =@ID";

                        var ship = await db.ExecuteAsync(shippingUpdate,
                            new
                            {

                                Id = codigo,
                                Weigth = weigth, 
                                Widith = widtih,
                                Length = length
});
                    }

                    var tagsInsert = "INSERT INTO dbo.\"Tag\" (\"TagName\") VALUES (@TagName)";

                    var tag = await db.ExecuteAsync(tagsInsert,
                        new
                        {
                            TagName = tags
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
