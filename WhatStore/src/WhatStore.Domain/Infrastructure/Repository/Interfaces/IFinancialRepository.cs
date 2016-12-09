﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Repository.Interfaces
{
    public interface IFinancialRepository
    {
        Task<bool> UpdateFinancial(string responsable, string name, string lastName, string cpf, string rg,
                                         DateTime birthDay, string gender, string ddd, string cnpj, string socialName,
                                         string stateIncentive, string municpalRegistration);

    }
}