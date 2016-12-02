using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Crosscutting.Infrastructure.Models.Localization;

namespace WhatStore.Crosscutting.Infrastructure.Repository.Interfaces
{
    public interface ILocalizationRepository
    {
        Task<List<State>> GetStates();
    }
}
