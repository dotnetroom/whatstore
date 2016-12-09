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

        public FinancialRepository(CustomSettings settings)
        {
            _settings = settings;
        }


       
    }
}
