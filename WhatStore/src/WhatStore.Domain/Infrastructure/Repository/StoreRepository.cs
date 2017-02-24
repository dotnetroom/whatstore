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
using WhatStore.Domain.Infrastructure.Models.Financial;
using Microsoft.AspNetCore.Http;
using WhatStore.Domain.Infrastructure.Models.Identity;

namespace WhatStore.Domain.Infrastructure.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private CustomSettings _settings;

        public StoreRepository(IOptions<CustomSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> UpdateStoreInformation(string fileName, long storeID, string storeName, string storeDescription, string phoneNumber, string email, string URL,
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
                            adressUpdate = "UPDATE dbo.Adress SET CEP = @CEP, CityID = @city, " +
                                       "Complement = @Complement, Number = @Number, Street = @Street " +
                                       "WHERE ID = @ID";

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
                            var insertAdress = InsertAdress(CEP, city, complemento, number, address);

                            adressUpdate = "UPDATE dbo.Store SET AdressId = @ADDRESS WHERE Store.Id = @ID";
                            var resultUpdateAddress = await db.ExecuteAsync(adressUpdate, new { ID = storeID, ADDRESS = insertAdress });
                        }
                    }
                    else
                    {
                        adressUpdate = "UPDATE dbo.Store SET AdressId = NULL WHERE Store.Id = @ID";
                        var resultUpdateAddress = await db.ExecuteAsync(adressUpdate, new { ID = storeID });
                    }
                    if (fileName != null)
                    {
                        var queryUpdateStore = "UPDATE dbo.Store SET Logo = @Logo, Description = @Description, Email = @Email, " +
                                               "Name = @Name, Phone = @Phone, Term = @Term, URL = @URl";

                        var resultUpdate = await db.ExecuteAsync(queryUpdateStore,
                                                                 new
                                                                 {
                                                                     Description = storeDescription,
                                                                     Email = email,
                                                                     Name = storeName,
                                                                     Phone = phoneNumber,
                                                                     Term = terms,
                                                                     URL = URL,
                                                                     Logo = fileName,
                                                                 });
                    }
                    else
                    {
                        var queryUpdateStore = "UPDATE dbo.Store SET Description = @Description, Email = @Email, " +
                                          "Name = @Name, Phone = @Phone, Term = @Term, URL = @URl WHERE Id = @StoreId";

                        var resultUpdate = await db.ExecuteAsync(queryUpdateStore,
                                                                 new
                                                                 {
                                                                     Description = storeDescription,
                                                                     Email = email,
                                                                     Name = storeName,
                                                                     Phone = phoneNumber,
                                                                     Term = terms,
                                                                     URL = URL,
                                                                     StoreId = storeID
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

        public async Task<bool> RegisterStoreType(string storeType)
        {

            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                await db.OpenAsync();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var queryInsertType = "INSERT INTO dbo.StoreType (Name) "
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
                    var result = await db.QueryAsync<StoreType>("SELECT * FROM dbo.StoreType");

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

                        var userExistent = await db.QueryAsync<ApplicationUser>("SELECT * FROM dbo.AspNetUser, dbo.Store WHERE dbo.Store.Id = dbo.AspNetUsers.StoreId");
                        var user = userExistent.FirstOrDefault();



                        if (user.Id <= 0)
                        {
                            await DeleteStore(user.StoreId);
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
                        var query = "SELECT dbo.Store.Name as StoreName, dbo.Store.Description as StoreDescription, dbo.Store.Email, dbo.Store.URL, dbo.Store.Logo as LogoName, dbo.Store.Term as Terms, dbo.Store.Phone as PhoneNumber, dbo.City.Name AS CityName, dbo.State.Id AS State, dbo.City.Id AS City, dbo.State.Name, dbo.Adress.Street as Address, dbo.Adress.Number, dbo.Adress.Complement as Complemento, dbo.Adress.CEP FROM dbo.Store, dbo.Adress, dbo.State, dbo.City WHERE dbo.Store.Id = @storeId AND Store.AdressId = Adress.Id AND Adress.CityID = City.Id AND City.StateId = State.Id";
                        var queryReturnData = await db.QueryAsync<RegisterStoreDataViewModel>(query, new { storeId = storeID });
                        var returnData = queryReturnData.FirstOrDefault();
                        returnData.PhoneDDD = subDDD;
                        returnData.PhoneNumber = subPhoneNumber;
                        returnData.Logo = store.Logo;
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
                        Logo = store.Logo
                    };


                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<bool> InsertFinancial(string CEP, int cityId, string complement, string number, string street, long storeId, string about,
                                                DateTime birthday, string CPF, string firstName, string lastName, bool isPessoaJuridica, string phone,
                                                string Rg, bool gender, string CNPJ, string razaoSocial, string inscricaoEstadual, string inscricaoMunicipal)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                await db.OpenAsync();

                try
                {

                    var getFinancial = await GetFinancial(storeId);

                    if (getFinancial != null)
                    {
                        var updateAdress = await UpdateAdress(getFinancial.AdressId, CEP, cityId, complement, number, street);

                        if (getFinancial.PessoaJuridicaId > 0)
                        {

                            var updatePessoaJuridica = await UpdatePessoaJuridica(getFinancial.PessoaJuridicaId, CNPJ, inscricaoEstadual, inscricaoMunicipal, razaoSocial);

                        }
                        else
                        {
                            var insertPessoaJuridica = await InsertPessoaJuridica(CNPJ, inscricaoEstadual, inscricaoMunicipal, razaoSocial);
                            var updateStorFinancial = UpdateStoreFinancial(insertPessoaJuridica, getFinancial.Id);
                        }

                        var updateStoreFinancial = await UpdateStoreFinancial(getFinancial.Id, about, birthday, CPF, firstName, lastName, gender, phone, Rg);

                    }
                    else
                    {
                        var insertAdress = await InsertAdress(CEP, cityId, complement, number, street);

                        if (isPessoaJuridica == true)
                        {
                            var insertPessoaJuridica = await InsertPessoaJuridica(CNPJ, inscricaoEstadual, inscricaoMunicipal, razaoSocial);
                        }

                        var insertStoreFinancial = await InsertStoreFinancial(storeId, insertAdress, about, birthday, CPF, firstName, lastName, gender, phone, Rg);

                    }

                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
            }
        }


        public async Task<RegisterStoreDataViewModel> GetAdress(long adressId)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    var query = "SELECT dbo.City.Name AS CityName, dbo.State.Id AS State, dbo.City.Id AS City, dbo.State.Name, dbo.Adress.Street as Address, dbo.Adress.Number, dbo.Adress.Complement as Complemento, dbo.Adress.CEP FROM dbo.Adress, dbo.State, dbo.City WHERE dbo.Adress.Id = @adressId AND Adress.CityID = City.Id AND City.StateId = State.Id";
                    var queryReturnData = await db.QueryAsync<RegisterStoreDataViewModel>(query, new { adressId = adressId });
                    return queryReturnData.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return new RegisterStoreDataViewModel();
                }
            }

        }


        public async Task<StoreFinancial> GetFinancial(long storeID)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    string subDDD = string.Empty;
                    string subPhone = string.Empty;

                    var resultStoreFinancial = await db.QueryAsync<StoreFinancial>("SELECT * FROM dbo.StoreFinancial WHERE dbo.StoreFinancial.StoreId = @ID", new { ID = storeID });
                    return resultStoreFinancial.FirstOrDefault();


                }
                catch (Exception ex)
                {
                    return new StoreFinancial();

                }
            }
        }

        public async Task<PessoaJuridica> GetPessoaJuridica(long pessoaJuridicaId)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    var queryPJ = "SELECT * FROM dbo.PessoaJuridica WHERE dbo.PessoaJuridica.Id = @ID";
                    var resultPJ = await db.QueryAsync<PessoaJuridica>(queryPJ, new { ID = pessoaJuridicaId });
                    return resultPJ.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return new PessoaJuridica();
                }
            }
        }

        public async Task<bool> UpdateAdress(long adressId, string CEP, int cityId, string complement, string number, string street)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
                {


                    var updateAdress = await db.ExecuteAsync("UPDATE dbo.Adress SET CEP = @CEP, CityID = @CityID, Complement = @Complement, Number = @Number, Street = @Street WHERE Id = @AdressId",
                           new
                           {
                               CEP = CEP,
                               CityID = cityId,
                               Complement = complement,
                               Number = number,
                               Street = street,
                               AdressId = adressId,
                           });

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<long> InsertAdress(string CEP, int cityId, string complement, string number, string street)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    var insertAdress = await db.QueryAsync<long>("INSERT INTO dbo.Adress (CEP, CityID, Complement, Number, Street) SET CEP = @Cep, CityID = @CityID, Complement = @Complement, Number = @ Number, Street = @Street; SELECT SCOPE_IDENTITY();",
                                                                new
                                                                {
                                                                    Cep = CEP,
                                                                    CityID = cityId,
                                                                    Complement = complement,
                                                                    Number = number,
                                                                    Street = street
                                                                });

                    return insertAdress.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }


        public async Task<bool> UpdatePessoaJuridica(long pessoaJuridicaId, string CNPJ, string inscricaoEstadual, string inscricaoMunicipal, string razaoSocial)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    var updatePessoaJuridica = await db.ExecuteAsync("UPDATE dbo.PessoaJuridica SET CNPJ = @CNPJ, InscricaoEstadual = @InscricaoEstadual, InscricaoMunicipal = @InscricaoMunicipal, RazaoSocial = @RazaoSocial  WHERE dbo.PessoaJuridica.Id = @PessoaJuridicaId",
                                                                     new
                                                                     {
                                                                         PessoaJuridicaId = pessoaJuridicaId,
                                                                         CNPJ = CNPJ,
                                                                         InscricaoEstadual = inscricaoEstadual,
                                                                         InscricaoMunicipal = inscricaoMunicipal,
                                                                         RazaoSocial = razaoSocial
                                                                     });
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<long> InsertPessoaJuridica(string CNPJ, string inscricaoEstadual, string inscricaoMunicipal, string razaoSocial)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
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

                    return resultInsertPessoaJuridica.FirstOrDefault();

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }


        public async Task<bool> UpdateStoreFinancial(long pessoaJuridicaId, long storeFinancialId)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    var queryUpdateStoreFinancial = "UPDATE dbo.StoreFinancial SET dbo.StoreFinancial.PessoaJuridicaId = @PessoaJuridicaId WHERE dbo.StoreFinancial.Id = @ID";

                    var result = await db.ExecuteAsync(queryUpdateStoreFinancial, new
                    {
                        PessoaJuridicaId = pessoaJuridicaId,
                        ID = storeFinancialId
                    });
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateStoreFinancial(long storeFinancialId, string about, DateTime birthday, string cpf, string firstName, string lastName, bool gender, string phone, string rg)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    var updatePessoaJuridica = await db.ExecuteAsync("UPDATE dbo.StoreFinancial SET About = @About, Birthday = @Birthday, CPF = @CPF, FirstName = @FirstName, Gender = @Gender, LastName = @LastName, Phone = @Phone, Rg = Rg WHERE dbo.StoreFinancial.Id = @ID",
                                                                    new
                                                                    {
                                                                        ID = storeFinancialId,
                                                                        About = about,
                                                                        Birthday = birthday,
                                                                        CPF = cpf,
                                                                        FirstName = firstName,
                                                                        Gender = gender,
                                                                        LastName = lastName,
                                                                        Phone = phone,
                                                                        Rg = rg
                                                                    });
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }



        public async Task<bool> InsertStoreFinancial(long storeId, long adressId, string about, DateTime birthday, string cpf, string firstName, string lastName, bool gender, string phone, string rg)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    var queryInsertStoreFinancial = "INSERT INTO dbo.StoreFinancial(About, AdressId, Birthday, CPF, FirstName, Gender, LastName, Phone, Rg, StoreId) " +
                                                     "VALUES (@About, @AdressId, @Birthday, @CPF, @FirstName, @Gender, @LastName, @Phone, @Rg, @StoreId); SELECT SCOPE_IDENTITY();";

                    var resultInsertStore = await db.ExecuteAsync(queryInsertStoreFinancial, new
                    {
                        About = about,
                        FirstName = firstName,
                        LastName = lastName,
                        CPF = cpf,
                        Rg = rg,
                        Birthday = birthday,
                        Gender = gender,
                        AdressId = adressId,
                        Phone = phone,
                        StoreId = storeId
                    });


                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public async Task<bool> DeleteStore(long storeId)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    var deleteStore = await db.ExecuteAsync("DELETE FROM dbo.Store WHERE dbo.Store.Id = @ID",
                        new
                        {
                            ID = storeId
                        });

                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteLogo(long storeId)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    var query = "UPDATE dbo.Store SET Logo = NULL WHERE dbo.Store.Id = @ID";
                    var updateLogo = await db.ExecuteAsync(query, new { ID = storeId });
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<string> GetLogo(string store)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    var query = "SELECT dbo.Store.Logo FROM dbo.Store WHERE dbo.Store.URL = @URL";
                    var logo = await db.QueryAsync<string>(query, new { URL = store });
                    return logo.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}





