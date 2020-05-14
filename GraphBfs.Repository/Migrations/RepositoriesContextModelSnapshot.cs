﻿// <auto-generated />
using GraphBfs.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GraphBfs.Migrations
{
    [DbContext(typeof(RepositoriesContext))]
    partial class RepositoriesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Repositories.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Repositories.Models.LogisticCenter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CheckSum")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LogisticCenters");
                });

            modelBuilder.Entity("Repositories.Models.Path", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EndCityId")
                        .HasColumnType("int");

                    b.Property<int?>("InitialCityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EndCityId");

                    b.HasIndex("InitialCityId");

                    b.ToTable("Paths");
                });

            modelBuilder.Entity("Repositories.Models.Path", b =>
                {
                    b.HasOne("Repositories.Models.City", "EndCity")
                        .WithMany()
                        .HasForeignKey("EndCityId");

                    b.HasOne("Repositories.Models.City", "InitialCity")
                        .WithMany()
                        .HasForeignKey("InitialCityId");
                });
#pragma warning restore 612, 618
        }
    }
}
