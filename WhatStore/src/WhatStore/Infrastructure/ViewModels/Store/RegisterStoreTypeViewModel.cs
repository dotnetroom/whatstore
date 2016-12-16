using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Store;

namespace WhatStore.Infrastructure.ViewModels.Store
{
    public class RegisterStoreTypeViewModel
    {
        public List<StoreType> StoreTypes { get; set; }

        public string StoreType { get; set; }

        public string ReturnMessage { get; set; }
    }
}
