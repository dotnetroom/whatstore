﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Localization;

namespace WhatStore.Domain.Infrastructure.ViewModels.Admin
{
    public class FinancialViewModel
    {
        public List<State> States { get; set; }
    }
}