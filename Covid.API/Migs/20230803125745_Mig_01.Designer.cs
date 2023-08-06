﻿// <auto-generated />
using System;
using Covid.API.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Covid.API.Migs
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230803125745_Mig_01")]
    partial class Mig_01
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Covid.API.Entities.CovidBilgi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Sayi")
                        .HasColumnType("integer")
                        .HasColumnName("sayi");

                    b.Property<int>("Sehir")
                        .HasColumnType("integer")
                        .HasColumnName("sehir");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("timestamp")
                        .HasColumnName("tarih");

                    b.HasKey("Id");

                    b.ToTable("covid_bilgi", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
