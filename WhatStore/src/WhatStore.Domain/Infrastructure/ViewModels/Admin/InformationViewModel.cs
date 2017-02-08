using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Localization;
using WhatStore.Domain.Infrastructure.Models.Product;


namespace WhatStore.Domain.Infrastructure.ViewModels.Admin
{
    public class InformationViewModel
    {
        public List<State> States { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
