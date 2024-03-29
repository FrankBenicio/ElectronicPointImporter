﻿// <auto-generated />
using System;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infra.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230308182252_001")]
    partial class _001
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Archive", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Directory")
                        .IsRequired()
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(2000)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Archives");
                });

            modelBuilder.Entity("Domain.Models.DepartmentPayment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArchiveId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("MonthTerm")
                        .IsRequired()
                        .HasColumnType("varchar(2000)");

                    b.Property<decimal>("TotalDiscounts")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalExtras")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalPay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("YearTerm")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArchiveId");

                    b.ToTable("DepartmentsPayment");
                });

            modelBuilder.Entity("Domain.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<double>("DebitHours")
                        .HasColumnType("float");

                    b.Property<Guid?>("DepartmentPaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("ExtraDays")
                        .HasColumnType("float");

                    b.Property<double>("MissingDays")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(2000)");

                    b.Property<double>("Overtime")
                        .HasColumnType("float");

                    b.Property<decimal>("TotalReceive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("WorkedDays")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentPaymentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Domain.Models.DepartmentPayment", b =>
                {
                    b.HasOne("Domain.Models.Archive", "Archive")
                        .WithMany()
                        .HasForeignKey("ArchiveId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Archive");
                });

            modelBuilder.Entity("Domain.Models.Employee", b =>
                {
                    b.HasOne("Domain.Models.DepartmentPayment", null)
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentPaymentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Domain.Models.DepartmentPayment", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
