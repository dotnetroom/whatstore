using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhatStore.Crosscutting.Infrastructure.Models.Identity;
using WhatStore.Crosscutting.Infrastructure.Models.Product;
using WhatStore.Crosscutting.Infrastructure.Models.Store;

namespace WhatStore.Models.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TagProduct>(x =>
            {
                x.HasKey(y => new { y.ProductId, y.TagId });
            });

            base.OnModelCreating(builder);
        }

        public DbSet<PictureProduct> PictureProduct { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductSize> ProductSize { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<TagProduct> TagProduct { get; set; }
        public DbSet<Adress> Adress { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<PessoaJuridica> PessoaJuridica { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<StoreType> StoreType { get; set; }


    }
}
