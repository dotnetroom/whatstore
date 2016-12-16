﻿using System;
using System.Linq;
using Dapper;
using System.Threading.Tasks;

using System.Data.SqlClient;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using WhatStore.Domain.Infrastructure.Contexts;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using WhatStore.Domain.Infrastructure.Models.Store;
using WhatStore.Domain.Infrastructure.ViewModels.Admin;

namespace WhatStore.Domain.Infrastructure.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private CustomSettings _settings;
        public StoreRepository(IOptions<CustomSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> UpdateStoreInformation(long storeID, string storeName, string storeDescription, string phoneNumber, string email, string URL,
                                                       string terms, bool hasAddress, string address, string number, string CEP, string complemento, int city)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                await db.OpenAsync();
                try
                {
                    
                    if (storeID <= 0) return false;

                    var adressUpdate = string.Empty;

                    if (hasAddress)
                    {
                        var resultAddressID = await db.QueryAsync<long>("SELECT dbo.Adress.Id FROM dbo.Adress, dbo.Store WHERE dbo.Store.AdressId = dbo.Adress.Id AND dbo.Store.ID = @STOREID",
                                                                  new { STOREID = storeID });

                        var addressID = resultAddressID.FirstOrDefault();

                        if (addressID > 0)
                        {
                            adressUpdate = "UPDATE dbo.\"Adress\" SET \"CEP\" = @CEP, \"CityID\" = @city, " +
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
                                                                            });
                        }

                        else
                        {
                            var queryInsertAddress = "INSERT INTO dbo.\"Adress\" (\"CEP\", \"CityID\", \"Complement\", \"Number\", \"Street\") "
                                                    + "VALUES (@CEP, @CITY, @COMPLEMENT, @NUMBER, @STREET)";

                            var resultInsertAddress = await db.ExecuteAsync(queryInsertAddress, new { CEP = CEP, CITY = city, COMPLEMENT = complemento, NUMBER = number, STREET = address });
                        }
                    }
                    else
                    {
                        adressUpdate = "UPDATE dbo.Store SET AdressId = NULL";
                        var resultUpdateAddress = await db.ExecuteAsync(adressUpdate);
                    }

                    var queryUpdateStore = "UPDATE dbo.\"Store\" SET \"Description\" = @Description, \"Email\" = @Email, " +
                                           "\"Name\" = @Name, \"Phone\" = @Phone, \"Term\" = @Term, \"URL\" = @URl";

                    var resultUpdate = await db.ExecuteAsync(queryUpdateStore,
                                                             new
                                                             {
                                                                 Description = storeDescription,
                                                                 Email = email,
                                                                 Name = storeName,
                                                                 Phone = phoneNumber,
                                                                 Term = terms,
                                                                 URL = URL,
                                                             });
                    
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public async Task<bool> RegisterStoreType(string storeType)
        {

            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                await db.OpenAsync();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var queryInsertType = "INSERT INTO dbo.\"StoreType\" (\"Name\") "
                                                            + "VALUES (@Name)";

                        var resultInsertType = await db.ExecuteAsync(queryInsertType, new { Name = storeType }, trans);
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return false;
                    }
                }

            }
        }

        public async Task<List<StoreType>> GetStoreType()
        {
            try
            {
                using (var db = new SqlConnection(_settings.ConnectionString))
                {
                    var result = await db.QueryAsync<StoreType>("SELECT * FROM \"dbo\".\"StoreType\"");

                    return result.ToList();
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<long> RegisterStore(Store store)
        {

            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                await db.OpenAsync();
                using (var trans = db.BeginTransaction())
                {


                    try
                    {
                        var queryInsertStore = "INSERT INTO dbo.Store (Name, URL, IsActive, StoreTypeId) " +
                                                "VALUES (@Name, @URL, @IsActive, @StoreTypeId); SELECT SCOPE_IDENTITY();";

                        var resultInsertStore = await db.QueryAsync<long>(queryInsertStore, store, trans);

                        var storeID = resultInsertStore.FirstOrDefault();

                        if (storeID > 0)
                        {
                            trans.Commit();
                            return storeID;
                        }

                        trans.Rollback();
                        return -1;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return -1;
                    }

                }
            }

        }

        public async Task<RegisterStoreDataViewModel> GetStore(long storeID)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    string subDDD = string.Empty;
                    string subPhoneNumber = string.Empty;
                    var queryStore = "SELECT * FROM dbo.Store WHERE dbo.Store.Id = @STOREID";
                    var queryStoreReturnData = await db.QueryAsync<Store>(queryStore, new { STOREID = storeID });
                    var store = queryStoreReturnData.FirstOrDefault();
                    if (store.Phone.Length > 0)
                    {
                        subDDD = store.Phone.Substring(0, 2);
                        subPhoneNumber = store.Phone.Substring(2);
                    }
                    if (store.AdressId != null && store.AdressId > 0)
                    {
                        var query = "SELECT dbo.Store.Name as StoreName, dbo.Store.Description as StoreDescription, dbo.Store.Email, dbo.Store.URL, dbo.Store.Term as Terms, dbo.Store.Phone as PhoneNumber, dbo.City.Name, dbo.State.Name, dbo.Adress.Street as Address, dbo.Adress.Number, dbo.Adress.Complement as Complemento, dbo.Adress.CEP FROM dbo.Store, dbo.Adress, dbo.State, dbo.City WHERE dbo.Store.Id = @storeId AND Store.AdressId = Adress.Id AND Adress.CityID = City.Id AND City.StateId = State.Id";
                        var queryReturnData = await db.QueryAsync<RegisterStoreDataViewModel>(query, new { storeId = storeID });
                        var returnData = queryReturnData.FirstOrDefault();
                        return returnData;
                    }

                    else return new RegisterStoreDataViewModel()
                    {
                        StoreName = store.Name,
                        StoreDescription = store.Description,
                        PhoneDDD = subDDD,
                        PhoneNumber = subPhoneNumber,
                        Email = store.Email,
                        URL = store.URL,
                        Terms = store.Term,                        
                    };

                    
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}


