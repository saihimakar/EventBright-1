using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions options) 
            : base(options)
        {

        }

        public DbSet<CatalogEventBrand> CatalogBrands { get; set; }
        public DbSet<CatalogEventType> CatalogTypes { get; set; }
        public DbSet<CatalogEventItem> CatalogItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogEventType>(ConfigureCatalogType);
            modelBuilder.Entity<CatalogEventBrand>(ConfigureCatalogBrand);
            modelBuilder.Entity<CatalogEventItem>(ConfigureCatalogItem);
        }

        private void ConfigureCatalogItem(EntityTypeBuilder<CatalogEventItem> builder)
        {
            builder.ToTable("Catalog");

            builder.Property(c => c.Id).IsRequired().ForSqlServerUseSequenceHiLo("catalog_hilo");
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Price).IsRequired();
            builder.HasOne(c => c.CatalogEventType).WithMany().HasForeignKey(c => c.CatalogTypeId);
            builder.HasOne(c => c.CatalogEventBrand).WithMany().HasForeignKey(c => c.CatalogBrandId);
        }

        private void ConfigureCatalogType(
            EntityTypeBuilder<CatalogEventType> builder)
        {
            builder.ToTable("CatalogTypes");

            builder.Property(c => c.Id).IsRequired().ForSqlServerUseSequenceHiLo("catalog_type_hilo");
            builder.Property(c => c.Type).IsRequired().HasMaxLength(100);
        }

        private void ConfigureCatalogBrand(EntityTypeBuilder<CatalogEventBrand> builder)
        {
            builder.ToTable("CatalogBrands");

            builder.Property(c => c.Id).IsRequired().ForSqlServerUseSequenceHiLo("catalog_brand_hilo");
            builder.Property(c => c.Brand).IsRequired().HasMaxLength(100);
        }

    }
}
