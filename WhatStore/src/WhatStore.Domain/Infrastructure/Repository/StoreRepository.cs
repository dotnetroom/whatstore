using System;
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
using WhatStore.Domain.Infrastructure.Models.Localization;

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
                                                    + "VALUES (@CEP, @CITYID, @COMPLEMENT, @NUMBER, @STREET) SELECT SCOPE_IDENTITY()";

                            var newAddress = new Adress()
                            {
                                CEP = CEP,
                                CityID = city,
                                Complement = complemento,
                                Number = number,
                                Street = address
                            };

                            var addressInstertedID = await db.ExecuteScalarAsync(queryInsertAddress, newAddress);

                            adressUpdate = "UPDATE dbo.Store SET AdressId = @ADDRESS WHERE Store.Id = @ID";
                            var resultUpdateAddress = await db.ExecuteAsync(adressUpdate, new { ID = storeID, ADDRESS = addressInstertedID });
                        }
                    }
                    else
                    {
                        adressUpdate = "UPDATE dbo.Store SET AdressId = NULL WHERE Store.Id = @ID";
                        var resultUpdateAddress = await db.ExecuteAsync(adressUpdate, new { ID = storeID });
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
        public async Task<bool> DeleteStoreType(int storeType)
        {
            try
            {
                using (var db = new SqlConnection(_settings.ConnectionString))
                {

                    var result = await db.ExecuteAsync("DELETE FROM dbo.StoreType WHERE dbo.StoreType.Id = @storeType", new { storeType = storeType });

                    return true;
                }

            }

            catch (Exception ex)
            {
                return false;
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
                    if (store.Phone != null && store.Phone.Length > 0)
                    {
                        subDDD = store.Phone.Substring(0, 2);
                        subPhoneNumber = store.Phone.Substring(2);
                    }
                    if (store.AdressId != null && store.AdressId > 0)
                    {
                        var query = "SELECT dbo.Store.Name as StoreName, dbo.Store.Description as StoreDescription, dbo.Store.Email, dbo.Store.URL, dbo.Store.Term as Terms, dbo.Store.Phone as PhoneNumber, dbo.City.Name AS CityName, dbo.State.Id AS State, dbo.City.Id AS City, dbo.State.Name, dbo.Adress.Street as Address, dbo.Adress.Number, dbo.Adress.Complement as Complemento, dbo.Adress.CEP FROM dbo.Store, dbo.Adress, dbo.State, dbo.City WHERE dbo.Store.Id = @storeId AND Store.AdressId = Adress.Id AND Adress.CityID = City.Id AND City.StateId = State.Id";
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

        public async Task<bool> InsertFinancial(string CEP, int cityId, string complement, string number, string street, long storeId, string about, 
                                                DateTime birthday, string CPF, string firstName,string lastName, bool isPessoaJuridica, string phone, 
                                                string Rg, bool gender, string CNPJ, string razaoSocial, string inscricaoEstadual, string inscricaoMunicipal)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                await db.OpenAsync();

                try
                {
                    long selectPessoaJuridicaId = 0;

                    if (isPessoaJuridica == true)
                    {
                        var queryInsertPessoaJuridica = "INSERT INTO dbo.PessoaJuridica (CNPJ, InscricaoEstadual, InscricaoMunicipal, RazaoSocial) "
                                                        + "VALUES (@CNPJ, @InscricaoEstadual, @InscricaoMunicipal, @RazaoSocial); SELECT SCOPE_IDENTITY()";

                        var resultInsertPessoaJuridica = await db.QueryAsync<long>(queryInsertPessoaJuridica,
                                                        new
                                                        {
                                                            CNPJ = CNPJ,
                                                            InscricaoEstadual = inscricaoEstadual,
                                                            InscricaoMunicipal = inscricaoMunicipal,
                                                            RazaoSocial = razaoSocial
                                                        });

                        selectPessoaJuridicaId = resultInsertPessoaJuridica.FirstOrDefault();
              
                    }

                    var queryInsertAddress = "INSERT INTO dbo.Adress (CEP, CityID, Complement, Number, Street) "
                                                   + "VALUES (@CEP, @CITYID, @COMPLEMENT, @NUMBER, @STREET); SELECT SCOPE_IDENTITY()";

                    var newAddress = new Adress()
                    {
                        CEP = CEP,
                        CityID = cityId,
                        Complement = complement,
                        Number = number,
                        Street = street
                    };

                    var adressInstertedID = await db.QueryAsync<long>(queryInsertAddress, newAddress);

                    var selectAdressId = adressInstertedID.FirstOrDefault();

                    var queryInsertStoreFinancial = "INSERT INTO dbo.StoreFinancial(About, AdressId, Birthday, CPF, FirstName, Gender, LastName, PessoaJuridicaId, Phone, Rg, StoreId) " +
                                                  "VALUES (@About, @AdressId, @Birthday, @CPF, @FirstName, @Gender, @LastName, @PessoaJuridicaId, @Phone, @Rg, @StoreId); SELECT SCOPE_IDENTITY();";

                    var resultInsertStore = await db.ExecuteAsync(queryInsertStoreFinancial, new
                    {
                        About = about,
                        FirstName = firstName,
                        LastName = lastName,
                        CPF = CPF,
                        Rg = Rg,
                        Birthday = birthday,
                        Gender = gender,
                        PessoaJuridicaId = selectPessoaJuridicaId,
                        AdressId = selectAdressId,
                        Phone = phone,
                        StoreId = storeId,
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



