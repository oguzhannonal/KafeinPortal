﻿// <auto-generated />
using KafeinPortal.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KafeinPortal.Data.Migrations
{
    [DbContext(typeof(EfContext))]
    [Migration("20210805160002_yeter")]
    partial class yeter
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("KafeinPortal.Data.Model.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Sector")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("KafeinPortal.Data.Model.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("KafeinPortal.Data.Model.Models.ProjectDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BackendTech")
                        .HasColumnType("text");

                    b.Property<string>("DatabaseTech")
                        .HasColumnType("text");

                    b.Property<string>("FrontendTech")
                        .HasColumnType("text");

                    b.Property<int>("ProductionTime")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectSize")
                        .HasColumnType("integer");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("text");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId")
                        .IsUnique();

                    b.ToTable("ProjectDetails");
                });

            modelBuilder.Entity("KafeinPortal.Data.Model.Models.Project", b =>
                {
                    b.HasOne("KafeinPortal.Data.Model.Models.Customer", "Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("KafeinPortal.Data.Model.Models.ProjectDetail", b =>
                {
                    b.HasOne("KafeinPortal.Data.Model.Models.Project", "Project")
                        .WithOne("ProjectDetails")
                        .HasForeignKey("KafeinPortal.Data.Model.Models.ProjectDetail", "ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("KafeinPortal.Data.Model.Models.Customer", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("KafeinPortal.Data.Model.Models.Project", b =>
                {
                    b.Navigation("ProjectDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
