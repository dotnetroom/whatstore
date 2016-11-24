<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
>>>>>>> origin/1.0

namespace WhatStore.Crosscutting.Infrastructure.Models.Product
{
    public class ProductSize
    {
        [Required]
        public string ProductId { get; set; }
        [Required]
        public int SizeId { get; set; }

        public virtual Product Product { get; set;}
        public virtual Size Size  { get; set; }
    }
}
