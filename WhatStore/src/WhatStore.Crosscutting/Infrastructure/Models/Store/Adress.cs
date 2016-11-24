using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Models.Store
{
    public class Adress
    {
        public long ID { get; set; }
        public int CityID { get; set; }
        public String CEP { get; set; }
        public String Number { get; set; }
        public String Street { get; set; }
        public String Complement { get; set; }

        public virtual City City { get; set; }
    }
}
