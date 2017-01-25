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
                    var result = await db.QueryAsync<Product>("SELECT * FROM dbo.Product WHERE dbo.Product.StoreID = @STOREID",
                        new
                        {
                            STOREID = storeID
                        });
                    var product = result.ToList();
                    return product;
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Product> GetProduct(string productID)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                var result = await db.QueryAsync<Product>("SELECT * FROM dbo.Product WHERE dbo.Product.Id= @ProductID ",
                    new
                    {
                        ProductID = productID,
                    });
                return result.FirstOrDefault();
            }
        }

        public async Task<bool> UpdateProduct(string productName, string description, double price, bool hasVariety, string colors,
                                              string sizes, bool isFreeShip, double length, double weigth, double widith,
                                              string tags, string id)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                await db.OpenAsync();
                try
                {                    

                    string[] arrayTag = tags.Split(',');

                    var productUpdateQuery = "UPDATE dbo.Product SET Description = @Description, IsFreeShipping = @IsFreeShipping, " +
                        "Length = @Length, Name = @Name, Price = @Price, Weigth = @Weigth, Widith = @Widith WHERE id = @Id";

                    var resultProductUpdate = await db.ExecuteAsync(productUpdateQuery,
                                                                    new
                                                                    {
                                                                        Id = id,
                                                                        Description = description,
                                                                        IsFreeShipping = isFreeShip,
                                                                        Length = (isFreeShip != false) ? length : 0,
                                                                        Name = productName,
                                                                        Price = price,
                                                                        Weigth = (isFreeShip != false) ? weigth : 0,
                                                                        Widith = (isFreeShip != false) ? widith : 0,
                                                                    });
                    for (int i = 0; i < arrayTag.Length; i++)
                    { 
                        var deleteTag = await db.ExecuteAsync("DELETE FROM dbo.TagProduct WHERE dbo.TagProduct.ProductId = @id", 
                            new
                            {
                                id = id
                            });
                    }
                    for (int i = 0; i < arrayTag.Length; i++)
                    {
                        var tagSelect = await db.QueryAsync<long>("SELECT dbo.Tag.TagId FROM dbo.Tag WHERE dbo.Tag.TagName = @TagName",
                            new
                            {
                                TagName = arrayTag[i]
                            });
                        var resultTagSelect = tagSelect.FirstOrDefault();

                        if (resultTagSelect < 1)
                        {
                            var tagInsertQuery = "INSERT INTO dbo.Tag (TagName) VALUES (@TagName); SELECT SCOPE_IDENTITY();";
                            var tagInsert = await db.QueryAsync<long>(tagInsertQuery,
                                                                    new
                                                                    {
                                                                        TagName = arrayTag[i]
                                                                    });
                            resultTagSelect = tagInsert.FirstOrDefault();


                        }

                        var tagProductInsert = "INSERT INTO dbo.TagProduct (ProductId, TagId) VALUES (@ProductId, @TagId)";
                        var tagProduct = await db.ExecuteAsync(tagProductInsert,
                            new
                            {
                                ProductId = id,
                                TagId = resultTagSelect
                            });
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


        public async Task<List<string>> GetTag(string productId)
        {
            List<string> tagName = new List<string>(); ;
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                await db.OpenAsync();
                try
                {
                  
                    var tagIdSelect = await db.QueryAsync<long>("SELECT dbo.TagProduct.TagId FROM dbo.TagProduct WHERE dbo.TagProduct.ProductId = @ProductId",
                                                              new
                                                              {
                                                                  ProductId = productId
                                                              });
                    var resultTagIdSelect = tagIdSelect.ToList();

                    for (int i = 0; i < resultTagIdSelect.Count; i++)
                    {
                        var tagNameSelect = await db.QueryAsync<string>("SELECT dbo.Tag.TagName FROM dbo.Tag WHERE dbo.Tag.TagId = @TagId",
                                                                        new
                                                                        {
                                                                            TagId = resultTagIdSelect[i]
                                                                        });
                        var resultTagNameSelect = tagNameSelect.FirstOrDefault();
                        tagName.Add(resultTagNameSelect);
                    }
                }
                catch(Exception ex)
                {

                }
            }
            return tagName;
        }


        public async Task<bool> InsertProduct(long storeId, string productName, string description, double price, ICollection<IFormFile> picture,
                                              bool hasVariety, string colors, string sizes, bool isFreeShip, double length, double weigth,
                                              double widtih, string tags, string id)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                await db.OpenAsync();
                try
                {
                    if (storeId == 0) return false;

                    string[] arrayTag = tags.Split(',');

                    var codigo = id.Replace(" ", "");

                    var productInsert = "INSERT INTO dbo.Product (Id, Name, Description, Price, StoreId, IsFreeShipping, Length, Weigth, Widith) "
                                   + "VALUES (@ID, @NAME, @DESCRIPTION, @PRICE, @STOREID, @ISFREESHIPPING, @Length, @Weigth, @Widith)";

                    var product = await db.ExecuteAsync(productInsert,
                        new
                        {
                            Id = codigo,
                            Name = productName,
                            Description = description,
                            Price = price,
                            StoreId = storeId,
                            IsFreeShipping = isFreeShip,
                            Length = (isFreeShip != false) ? length : 0,
                            Weigth = (isFreeShip != false) ? weigth : 0,
                            Widith = (isFreeShip != false) ? widtih : 0
                        });


                    for (int i = 0; i < arrayTag.Length; i++)
                    {
                        var tagSelect = await db.QueryAsync<long>("SELECT dbo.Tag.TagId FROM dbo.Tag WHERE dbo.Tag.TagName = @TagName",
                            new
                            {
                                TagName = arrayTag[i]
                            });
                        var resultTagSelect = tagSelect.FirstOrDefault();

                        if (resultTagSelect < 1)
                        {
                            var tagInsertQuery = "INSERT INTO dbo.Tag (TagName) VALUES (@TagName); SELECT SCOPE_IDENTITY();";
                            var tagInsert = await db.QueryAsync<long>(tagInsertQuery,
                                                                    new
                                                                    {
                                                                        TagName = arrayTag[i]
                                                                    });
                            resultTagSelect = tagInsert.FirstOrDefault();


                        }

                        var tagProductInsert = "INSERT INTO dbo.TagProduct (ProductId, TagId) VALUES (@ProductId, @TagId)";
                        var tagProduct = await db.ExecuteAsync(tagProductInsert,
                            new
                            {
                                ProductId = id,
                                TagId = resultTagSelect
                            });
                    }


                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteProduct(string id)
        {
            try
            {
                using (var db = new SqlConnection(_settings.ConnectionString))
                {
                    var result = await db.ExecuteAsync("DELETE FROM dbo.Product WHERE dbo.Product.Id = @ID",
                        new
                        {
                            ID = id
                        });
                    return true;

                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
