﻿// <auto-generated />
using System;
using GestionEmployes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestionEmployes.Migrations
{
    [DbContext(typeof(GestionDbContext))]
    partial class GestionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GestionEmployes.Models.Departement", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentID"), 1L, 1);

                    b.Property<string>("NomDepartement")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("DepartmentID");

                    b.ToTable("Departements", "HR");
                });

            modelBuilder.Entity("GestionEmployes.Models.Employe", b =>
                {
                    b.Property<int>("EmployeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeId"), 1L, 1);

                    b.Property<DateTime>("DateEmbauche")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateNaissance")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartementId")
                        .HasColumnType("int");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("NomEmploye")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("Salaire")
                        .HasColumnType("decimal(12,2)");

                    b.Property<string>("UserImage")
                        .HasColumnType("varchar(200)");

                    b.HasKey("EmployeId");

                    b.HasIndex("DepartementId");

                    b.ToTable("Employes", "HR");
                });

            modelBuilder.Entity("GestionEmployes.Models.Tache", b =>
                {
                    b.Property<int>("TacheId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TacheId"), 1L, 1);

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(500)");

                    b.Property<int>("EmployeId")
                        .HasColumnType("int");

                    b.Property<string>("NomTache")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Priorite")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("TacheId");

                    b.HasIndex("EmployeId");

                    b.ToTable("Taches", "HR");
                });

            modelBuilder.Entity("GestionEmployes.Models.Employe", b =>
                {
                    b.HasOne("GestionEmployes.Models.Departement", "Departement")
                        .WithMany()
                        .HasForeignKey("DepartementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departement");
                });

            modelBuilder.Entity("GestionEmployes.Models.Tache", b =>
                {
                    b.HasOne("GestionEmployes.Models.Employe", "Employe")
                        .WithMany()
                        .HasForeignKey("EmployeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employe");
                });
#pragma warning restore 612, 618
        }
    }
}
