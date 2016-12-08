using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Crosscutting.Infrastructure.Repository.Interfaces;

namespace WhatStore.Crosscutting.Infrastructure.Repository
{
    public class FinancialRopository : IFinancialRepository
    {
        public Task<bool> SetFinancial(string responsable, string name, string lastName, string cpf, string rg, DateTime birthDay, string gender, string ddd, string cnpj, string socialName, string stateIncentive, string municpalRegistration, string cep, int state, int city, string address, string number, string complement)
        {
            throw new NotImplementedException();
        }
    }
}
