using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Models.Store
{
    public class City
    {
        public int CityID { get; set; }
        public int StateID { get; set; }
        public String Name { get; set; }

        public virtual State State { get; set; }
    }
}
