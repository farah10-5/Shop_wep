﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopPersistance;

#nullable disable

namespace ShopPersistance.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20240615012636_Inital")]
    partial class Inital
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShopDomain.Entities.Flower", b =>
                {
                    b.Property<int>("FlowerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlowerId"));

                    b.Property<string>("FlowerDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlowerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlowertPrice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GiftId")
                        .HasColumnType("int");

                    b.HasKey("FlowerId");

                    b.HasIndex("GiftId");

                    b.ToTable("Flowers");
                });

            modelBuilder.Entity("ShopDomain.Entities.Gift", b =>
                {
                    b.Property<int>("GiftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GiftId"));

                    b.Property<string>("GiftDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GiftName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GiftPrice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GiftType")
                        .HasColumnType("int");

                    b.HasKey("GiftId");

                    b.ToTable("Gifts");
                });

            modelBuilder.Entity("ShopDomain.Entities.Flower", b =>
                {
                    b.HasOne("ShopDomain.Entities.Gift", "Gift")
                        .WithMany("Flowers")
                        .HasForeignKey("GiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gift");
                });

            modelBuilder.Entity("ShopDomain.Entities.Gift", b =>
                {
                    b.Navigation("Flowers");
                });
#pragma warning restore 612, 618
        }
    }
}
