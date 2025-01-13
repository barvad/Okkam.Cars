using Microsoft.EntityFrameworkCore;
using Okkam.Cars.Ef.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Okkam.Cars.Ef.EntitiesConfigurations
{
    public class BrandEntityConfiguration : IEntityTypeConfiguration<BrandEntity>
    {
        public void Configure(EntityTypeBuilder<BrandEntity> builder)
        {
            builder.ToTable("Brands");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(200);
           /* builder.Property(p => p.Id).ValueGeneratedOnAdd().HasAnnotation("Npgsql:ValueGenerationStrategy",
                NpgsqlValueGenerationStrategy.SerialColumn);
            builder.Property(e => e.Id).UseIdentityAlwaysColumn();*/
        }
    }
}
