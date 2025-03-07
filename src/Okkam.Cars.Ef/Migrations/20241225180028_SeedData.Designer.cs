﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Okkam.Cars.Ef;

#nullable disable

namespace Okkam.Cars.Ef.Migrations
{
    [DbContext(typeof(CarsDbContext))]
    [Migration("20241225180028_SeedData")]
    partial class SeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Okkam.Cars.Ef.Entities.BodyStyleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("BodyStyles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Седан"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Хэтчбек"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Универсал"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Минивэн"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Внедорожник"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Купе"
                        });
                });

            modelBuilder.Entity("Okkam.Cars.Ef.Entities.BrandEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("Brands", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Audi"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ford"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Jeep"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Nissan"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Toyota"
                        });
                });

            modelBuilder.Entity("Okkam.Cars.Ef.Entities.CarEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("BodyStyleId")
                        .HasColumnType("integer");

                    b.Property<int>("BrandId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte>("SeatsCount")
                        .HasColumnType("smallint");

                    b.Property<string>("Url")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.HasKey("Id");

                    b.HasIndex("BodyStyleId");

                    b.HasIndex("BrandId");

                    b.HasIndex("BodyStyleId", "BrandId", "SeatsCount", "Name")
                        .IsUnique();

                    b.ToTable("Cars", (string)null);
                });

            modelBuilder.Entity("Okkam.Cars.Ef.Entities.CarEntity", b =>
                {
                    b.HasOne("Okkam.Cars.Ef.Entities.BodyStyleEntity", "BodyStyle")
                        .WithOne()
                        .HasForeignKey("Okkam.Cars.Ef.Entities.CarEntity", "BodyStyleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Okkam.Cars.Ef.Entities.BrandEntity", "Brand")
                        .WithOne()
                        .HasForeignKey("Okkam.Cars.Ef.Entities.CarEntity", "BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BodyStyle");

                    b.Navigation("Brand");
                });
#pragma warning restore 612, 618
        }
    }
}
