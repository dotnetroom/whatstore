using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhatStore.Crosscutting.Infrastructure.Models.Identity;
using WhatStore.Crosscutting.Infrastructure.Models.Product;

namespace WhatStore.Models.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<PictureProduct> PictureProduct { get; set; }
        public DbSet<Product.Product> Product { get; set; }


    }
}
