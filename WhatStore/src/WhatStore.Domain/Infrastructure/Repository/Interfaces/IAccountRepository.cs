using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Domain.Infrastructure.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task<string> GetUser(string email);
    }
}
