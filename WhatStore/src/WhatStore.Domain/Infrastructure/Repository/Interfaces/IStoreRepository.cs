using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Financial;
using WhatStore.Domain.Infrastructure.Models.Store;
using WhatStore.Domain.Infrastructure.ViewModels.Admin;

namespace WhatStore.Domain.Infrastructure.Repository.Interfaces
{
    public interface IStoreRepository
    {        
        Task<bool> DeleteStoreType(int storeType);
        Task<bool> DeleteLogo(long storeId);
        Task<bool> DeleteStore(long storeId);
        Task<bool> RegisterStoreType(string type);
        Task<long> RegisterStore(Store store);
        Task<RegisterStoreDataViewModel> GetStore(long storeID);        
        Task<bool> InsertFinancial(string CEP, int cityId, string complement, string number, string street, long storeId, string about,
                                    DateTime birthday, string CPF, string firstName, string lastName, bool isPessoaJuridica, string phone,
                                    string Rg, bool gender, string CNPJ, string socialName, string stateIncentive, string municipalRegistration);
        Task<StoreFinancial> GetFinancial(long storeID);
        Task<List<StoreType>> GetStoreType();
        Task<RegisterStoreDataViewModel> GetAdress(long adressId);
        Task<PessoaJuridica> GetPessoaJuridica(long pessoaJuridicaId);
        Task<bool> InsertStoreFinancial(long storeId, long adressId, string about, DateTime birthday, string cpf, string firstName, string lastName, bool gender, string phone, string rg);
        Task<bool> UpdateStoreFinancial(long storeFinancialId, string about, DateTime birthday, string cpf, string firstName, string lastName, bool gender, string phone, string rg);
        Task<long> InsertPessoaJuridica(string CNPJ, string inscricaoEstadual, string inscricaoMunicipal, string razaoSocial);
        Task<bool> UpdatePessoaJuridica(long pessoaJuridicaId, string CNPJ, string inscricaoEstadual, string inscricaoMunicipal, string razaoSocial);
        Task<long> InsertAdress(string CEP, int cityId, string complement, string number, string street);
        Task<bool> UpdateAdress(long adressId, string CEP, int cityId, string complement, string number, string street);
        Task<bool> UpdateStoreFinancial(long pessoaJuridicaId, long storeFinancialId);
        Task<bool> UpdateStoreInformation(string fileName, long userID, string storeName, string storeDescription, string phoneNumber, string email,
                                  string terms, bool hasAddress, string address, string number, string CEP, string complemento, int city);
        Task<string> SelectStoreName(string storeName);
    }
}
