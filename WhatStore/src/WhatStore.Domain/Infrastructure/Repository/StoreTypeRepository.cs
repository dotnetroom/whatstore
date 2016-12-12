using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Contexts;
using WhatStore.Domain.Infrastructure.Models.Store;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;

namespace WhatStore.Crosscutting.Infrastructure.Repository
{
    public class StoreTypeRepository : IStoreTypeRepository
    {
        #region Constructors and Injections

        private CustomSettings _settings;

        public StoreTypeRepository(IOptions<CustomSettings> settings)
        {
            _settings = settings.Value;
        }

        #endregion

        public async Task<List<StoreType>> GetTypes()
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
    }
}
