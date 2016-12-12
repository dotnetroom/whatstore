using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using WhatStore.Domain.Infrastructure.Contexts;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using WhatStore.Domain.Infrastructure.Models.Localization;

namespace WhatStore.Domain.Infrastructure.Repository
{
    public class LocalizationRepository : ILocalizationRepository
    {
        #region Constructors and Injections

        private CustomSettings _settings;

        public LocalizationRepository(IOptions<CustomSettings> settings)
        {
            _settings = settings.Value;
        }

        #endregion

        public async Task<List<State>> GetStates()
        {
            try
            {
                using (var db = new SqlConnection(_settings.ConnectionString))
                {
                    var result = await db.QueryAsync<State>("SELECT * FROM \"dbo\".\"State\"");

                    return result.ToList();
                }
            }

            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<List<City>> GetCities(int stateID)
        {
            try
            {
                using (var db = new SqlConnection(_settings.ConnectionString))
                {
                    var result = await db.QueryAsync<City>("SELECT * FROM \"dbo\".\"City\" WHERE \"StateId\" = @STATE", new { STATE = stateID });

                    return result.ToList();
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        Task<List<State>> ILocalizationRepository.GetStates()
        {
            throw new NotImplementedException();
        }

        Task<List<City>> ILocalizationRepository.GetCities(int stateID)
        {
            throw new NotImplementedException();
        }
    }
}
