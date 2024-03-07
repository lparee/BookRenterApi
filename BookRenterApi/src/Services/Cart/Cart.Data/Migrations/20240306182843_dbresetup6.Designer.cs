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
    [Migration("20240306182843_dbresetup6")]
    partial class dbresetup6
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

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("BookId")
                        .HasName("PK_Book_BookId");

                    b.ToTable("BookInventory", (string)null);

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            Author = "Chetan",
                            BookName = "Hamlet",
                            Price = 100m,
                            Quantity = 3
                        },
                        new
                        {
                            BookId = 2,
                            Author = "Bhagat",
                            BookName = "Hamlet1",
                            Price = 101m,
                            Quantity = 4
                        },
                        new
                        {
                            BookId = 3,
                            Author = "Amish",
                            BookName = "Hamlet2",
                            Price = 102m,
                            Quantity = 5
                        },
                        new
                        {
                            BookId = 4,
                            Author = "Harrish",
                            BookName = "Hamlet3",
                            Price = 103m,
                            Quantity = 2
                        },
                        new
                        {
                            BookId = 5,
                            Author = "Martin",
                            BookName = "Hamlet4",
                            Price = 104m,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("Carts.Core.Entities.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CartId")
                        .HasName("PK_Cart_CartId");

                    b.ToTable("Cart", (string)null);
                });

            modelBuilder.Entity("Carts.Core.Entities.CartBookMapping", b =>
                {
                    b.Property<int>("CBMappingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CBMappingId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.HasKey("CBMappingId")
                        .HasName("PK_MapId");

                    b.HasIndex("CartId");

                    b.ToTable("CartBookMapping", (string)null);
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

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            DisplayName = "Guest",
                            Email = "abc@bca.com",
                            FirstName = "Guest",
                            LastName = ""
                        },
                        new
                        {
                            UserId = 2,
                            DisplayName = "Robert",
                            Email = "robert@martin.com",
                            FirstName = "Robert",
                            LastName = "Martin"
                        });
                });

            modelBuilder.Entity("Carts.Core.Entities.CartBookMapping", b =>
                {
                    b.HasOne("Carts.Core.Entities.BookInventory", "Books")
                        .WithMany("Mappings")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Book_Map");

                    b.HasOne("Carts.Core.Entities.Cart", "Carts")
                        .WithMany("Mappings")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Carts_Map");

                    b.Navigation("Books");

                    b.Navigation("Carts");
                });

            modelBuilder.Entity("Carts.Core.Entities.BookInventory", b =>
                {
                    b.Navigation("Mappings");
                });

            modelBuilder.Entity("Carts.Core.Entities.Cart", b =>
                {
                    b.Navigation("Mappings");
                });
#pragma warning restore 612, 618
        }
    }
}
