using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Okkam.Cars.Ef.Entities;

namespace Okkam.Cars.Ef.EntitiesConfigurations;

public class CarsEntityConfiguration : IEntityTypeConfiguration<CarEntity>
{
    public void Configure(EntityTypeBuilder<CarEntity> builder)
    {
        builder.ToTable("Cars");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(1000);
        builder.Property(p => p.Url).HasMaxLength(1000).IsRequired(false);
        builder
            .HasOne<BodyStyleEntity>(x=>x.BodyStyle)
            .WithOne()
        .HasForeignKey<CarEntity>(x => x.BodyStyleId);
        builder.HasIndex(e => e.BodyStyleId).IsUnique(false);
        builder
            .HasOne(x => x.Brand)
            .WithOne()
            .HasForeignKey<CarEntity>(x => x.BrandId);
        builder.HasIndex(e => e.BrandId).IsUnique(false);
        builder.Property(p => p.Id).ValueGeneratedOnAdd().HasAnnotation("Npgsql:ValueGenerationStrategy",
            NpgsqlValueGenerationStrategy.SerialColumn);
        builder.Property(e => e.Id).UseIdentityAlwaysColumn();
        builder.HasIndex(u => new { u.BodyStyleId, u.BrandId, u.SeatsCount, u.Name }).IsUnique(true);
    }
}