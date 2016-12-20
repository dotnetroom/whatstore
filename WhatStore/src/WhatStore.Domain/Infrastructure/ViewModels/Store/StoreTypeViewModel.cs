using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Store;

namespace WhatStore.Domain.Infrastructure.ViewModels.Store
{
    public class StoreTypeViewModel
    {
        public List<StoreType> Types { get; set; }

        public string StoreType { get; set; }
    }
}
