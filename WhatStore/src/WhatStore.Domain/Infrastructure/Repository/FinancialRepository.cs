using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Crosscutting.Infrastructure.Contexts;
using WhatStore.Crosscutting.Infrastructure.Repository.Interfaces;

namespace WhatStore.Crosscutting.Infrastructure.Repository
{
    public class FinancialRepository : IFinancialRepository
    {

        private CustomSettings _settings;

        public FinancialRepository(IOptions<CustomSettings> settings)
        {
            _settings = settings.Value;
        }


        public Task<bool> UpdateFinancial(string responsable, string name, string lastName, string cpf, string rg, 
                                         DateTime birthDay, string gender, string ddd, string cnpj, string socialName, 
                                         string stateIncentive, string municpalRegistration)
        {
            
        }
    }
}
