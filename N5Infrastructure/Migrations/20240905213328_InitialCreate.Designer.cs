﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using N5Infrastructure.Data;

#nullable disable

namespace N5Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230726173328_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("N5Domain.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EmployeeForename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EmployeeForename");

                    b.Property<string>("EmployeeSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EmployeeSurname");

                    b.Property<DateTime>("PermissionDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("PermissionDate");

                    b.Property<int>("PermissionTypeId")
                        .HasColumnType("int")
                        .HasColumnName("PermissionTypeId");

                    b.HasKey("Id");

                    b.HasIndex("PermissionTypeId");

                    b.ToTable("Permissions", (string)null);
                });

            modelBuilder.Entity("N5Domain.Entities.PermissionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PermissionTypes");
                });

            modelBuilder.Entity("N5Domain.Entities.Permission", b =>
                {
                    b.HasOne("N5Domain.Entities.PermissionType", null)
                        .WithMany("Permissions")
                        .HasForeignKey("PermissionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("N5Domain.Entities.PermissionType", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
