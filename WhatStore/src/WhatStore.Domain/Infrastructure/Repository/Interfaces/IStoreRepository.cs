using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Store;
using WhatStore.Domain.Infrastructure.ViewModels.Admin;

namespace WhatStore.Domain.Infrastructure.Repository.Interfaces
{
    public interface IStoreRepository
    {
        Task<bool> UpdateStoreInformation(long userID, string storeName, string storeDescription, string phoneNumber, string email, string URL,
                                          string terms, bool hasAddress, string address, string number, string CEP, string complemento, int city);
        Task<List<StoreType>> GetStoreType();
        Task<bool> DeleteStoreType(long storeId);
        Task<bool> RegisterStoreType(string type);
        //Task<bool> RegisterStoreType(List<StoreType> types);
        Task<RegisterStoreDataViewModel> GetStore(long storeID);
        Task<long> RegisterStore(Store store);
    }

}
