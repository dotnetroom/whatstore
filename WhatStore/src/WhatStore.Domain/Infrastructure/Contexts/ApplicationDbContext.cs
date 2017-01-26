﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatStore.Domain.Infrastructure.Models.Identity;
using WhatStore.Domain.Infrastructure.Models.Localization;
using WhatStore.Domain.Infrastructure.Models.Product;
using WhatStore.Domain.Infrastructure.Models.Store;
using WhatStore.Domain.Infrastructure.Models.Financial;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WhatStore.Domain.Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TagProducts>(x =>
            {
                x.HasKey(y => new { y.ProductId, y.TagId });
            });
            builder.Entity<StoreFinancial>(x =>
            {
                x.HasOne(y => y.Store).WithMany().HasForeignKey("StoreId").OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(builder);
        }

        public DbSet<PictureProduct> PictureProduct { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductSize> ProductSize { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<TagProducts> TagProduct { get; set; }
        public DbSet<Adress> Adress { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<PessoaJuridica> PessoaJuridica { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<StoreType> StoreType { get; set; }
        public DbSet<StoreFinancial> StoreFinancial { get; set; }
    }
}
