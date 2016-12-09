﻿using System;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using WhatStore.Crosscutting.Infrastructure.Contexts;
using WhatStore.Crosscutting.Infrastructure.Repository.Interfaces;
using System.Data.SqlClient;

namespace WhatStore.Crosscutting.Infrastructure.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private CustomSettings _settings;
        public StoreRepository(CustomSettings settings)
        {
            _settings = settings;
        }

        public async Task<bool> UpdateStoreInformation(long userID, string storeName, string storeDescription, string phoneNumber, string email, string URL,
                                                       string terms, bool hasAddress, string address, string number, string CEP, string complemento, int city)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                await db.OpenAsync();
                using(var trans = db.BeginTransaction())
                {
                    try
                    {
                        var resultStore = await db.QueryAsync<long>("SELECT dbo.\"AspNetUsers\".\"StoreID\" FROM dbo.\"Store\", dbo.\"AspNetUsers\" WHERE" +
                            " dbo.\"AspNetUsers\".\"StoreID\" = dbo.\"Store\".\"ID\" AND dbo.\"AspNetUsers\".\"ID\" = @userID", new {userID = userID});

                        var storeID = resultStore.FirstOrDefault();

                        if (storeID <= 0) return false;

                        var adressUpdate = string.Empty;

                        if (hasAddress)
                        {
                            var resultAddressID = await db.QueryAsync<long>("SELECT dbo.\"Address\".\"ID\" FROM dbo.\"Address\", dbo.\"Store\" WHERE dbo.\"Store\".\"AddressID\" = dbo.\"Address\".\"ID\" AND dbo.\"Store\".\"ID\" = @STOREID",
                                                                      new { STOREID = storeID });

                            var addressID = resultAddressID.FirstOrDefault();

                            if(addressID > 0)
                            {
                                adressUpdate = "UPDATE dbo.\"Address\" SET \"CEP\" = @CEP, \"CityID\" = @city, " +
                                           "\"Complement\" = @Complement, \"Number\" = @Number, \"Street\" = @Street " +
                                           "WHERE \"ID\" = @ID";

                                var resultUpdateAddress = await db.ExecuteAsync(adressUpdate,
                                                                                new
                                                                                {
                                                                                    CEP = CEP,
                                                                                    city = city,
                                                                                    Complement = complemento,
                                                                                    Number = number,
                                                                                    Street = address,
                                                                                    ID = addressID
                                                                                }, trans);
                            }

                            else
                            {
                                var queryInsertAddress = "INSERT INTO dbo.\"Address\" (\"CEP\", \"CityID\", \"Complement\", \"Number\", \"Street\") "
                                                        + "VALUES (@CEP, @CITY, @COMPLEMENT, @NUMBER, @STREET)";

                                var resultInsertAddress = await db.ExecuteAsync(queryInsertAddress, new { CEP = CEP, CITY = city, COMPLEMENT = complemento, NUMBER = number, STREET = address }, trans);
                            }
                        }
                        else
                        {
                            adressUpdate = "UPDATE dbo.\"Store\" SET \"AddressID\" = NULL";
                            var resultUpdateAddress = await db.ExecuteAsync(adressUpdate, trans);
                        }

                        var queryUpdateStore = "UPDATE dbo.\"Store\" SET \"Description\" = @Description, \"Email\" = @Email, " +
                                               "\"Name\" = @Name, \"Phone\" = @Phone, \"Term\" = @Term, \"URL\" = @URl";

                        var resultUpdate = await db.ExecuteAsync(queryUpdateStore,
                                                                 new {
                                                                     Description = storeDescription,
                                                                     Email = email,
                                                                     Name = storeName,
                                                                     Phone = phoneNumber,
                                                                     Term = terms,
                                                                     URL = URL,
                                                                 }, trans);

                        trans.Commit();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }

        }



    }
}