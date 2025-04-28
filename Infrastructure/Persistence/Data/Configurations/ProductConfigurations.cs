using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Persistence.Data.Configurations
{
    class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            //Product <=> Brand Relationship
            builder.HasOne(P => P.ProductBrand)
                   .WithMany()
                   .HasForeignKey(P => P.BrandId);

            //Product <=> Type Relationship

            builder.HasOne(P => P.ProductType)
               .WithMany()
               .HasForeignKey(P => P.TypeId);

            builder.Property(P => P.Price)
                    .HasColumnType("decimal(10,2)");

        }
    }
}
