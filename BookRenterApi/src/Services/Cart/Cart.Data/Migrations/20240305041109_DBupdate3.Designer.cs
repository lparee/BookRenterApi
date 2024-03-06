﻿// <auto-generated />
using System;
using Carts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Carts.Data.Migrations
{
    [DbContext(typeof(BookRenterDbContext))]
    [Migration("20240305041109_DBupdate3")]
    partial class DBupdate3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Carts.Core.Entities.BookInventory", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("BookId")
                        .HasName("PK_Book_BookId");

                    b.HasIndex("CartId");

                    b.ToTable("BookInventory", (string)null);
                });

            modelBuilder.Entity("Carts.Core.Entities.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserProfileUserId")
                        .HasColumnType("int");

                    b.HasKey("CartId")
                        .HasName("PK_Cart_CartId");

                    b.HasIndex("UserProfileUserId");

                    b.ToTable("Cart", (string)null);
                });

            modelBuilder.Entity("Carts.Core.Entities.UserProfile", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("Guest");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId")
                        .HasName("PK_UserProfile_UserId");

                    b.ToTable("UserProfile", (string)null);
                });

            modelBuilder.Entity("Carts.Core.Entities.BookInventory", b =>
                {
                    b.HasOne("Carts.Core.Entities.Cart", "Carts")
                        .WithMany("Books")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Carts_Book");

                    b.Navigation("Carts");
                });

            modelBuilder.Entity("Carts.Core.Entities.Cart", b =>
                {
                    b.HasOne("Carts.Core.Entities.UserProfile", null)
                        .WithMany("Carts")
                        .HasForeignKey("UserProfileUserId");
                });

            modelBuilder.Entity("Carts.Core.Entities.Cart", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Carts.Core.Entities.UserProfile", b =>
                {
                    b.Navigation("Carts");
                });
#pragma warning restore 612, 618
        }
    }
}