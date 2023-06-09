﻿// <auto-generated />
using ApiSupplyChain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiSupplyChain.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230321031002_SegundTabela")]
    partial class SegundTabela
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ApiSupplyChain.Data.Estoque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Estoque");
                });

            modelBuilder.Entity("ApiSupplyChain.Data.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Register_Number")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Produtos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Macacão de proteção individual",
                            Manufacturer = "Empresa XPTO",
                            Name = "Macacão RF",
                            Register_Number = 1,
                            Type = "EPI"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Óculos de proteção individual",
                            Manufacturer = "Empresa XPTO",
                            Name = "Óculos de proteção",
                            Register_Number = 2,
                            Type = "EPI"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Bota de proteção individual",
                            Manufacturer = "Empresa XPTO",
                            Name = "Bota",
                            Register_Number = 3,
                            Type = "EPI"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
