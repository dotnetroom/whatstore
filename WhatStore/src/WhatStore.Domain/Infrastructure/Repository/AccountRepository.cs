using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Contexts;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;

namespace WhatStore.Domain.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private CustomSettings _settings;
        public AccountRepository(IOptions<CustomSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<string> GetUser(string email)
        {
            try
            {
                using (var db = new SqlConnection(_settings.ConnectionString))
                {
                    var selectUser = await db.QueryAsync<string>("SELECT dbo.AspNetUser.Email FROM dobo.AspNetUser WHERE dbo.AspNetUser.Email = @Emai",
                        new
                    {
                            Email = email
                    });

                    return selectUser.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
