using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Store;

namespace WhatStore.Domain.Infrastructure.Repository.Interfaces
{
    interface IStoreTypeRepository
    {
        Task<List<StoreType>> GetTypes();
    }
}
