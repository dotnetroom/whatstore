using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Store;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;

namespace WhatStore.Crosscutting.Infrastructure.Repository
{
    public class StoreTypeRepository : IStoreTypeRepository
    {
        public Task<List<StoreType>> GetTypes()
        {
            try
            {
                
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
