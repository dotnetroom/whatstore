using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Domain.Infrastructure.ViewModels.Admin
{
    public class PictureProductViewModel
    {
        public long Id { get; set; }

        public string ImageName { get; set; }

        public string ProductId { get; set; }
    }
}
