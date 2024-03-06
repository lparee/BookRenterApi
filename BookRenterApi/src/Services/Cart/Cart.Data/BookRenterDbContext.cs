using Carts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Carts.Data;

public partial class BookRenterDbContext : DbContext
{
    public BookRenterDbContext()
    {
    }

    public BookRenterDbContext(DbContextOptions<BookRenterDbContext> options)
        : base(options)
    {
    }

    //override on configuration for scafolding EF core DB
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Initial Catalog=BookRenterDB;Integrated Security=SSPI; MultipleActiveResultSets=true;TrustServerCertificate=True;");


    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<CartBookMapping> CartBookMapping { get; set; }
    public virtual DbSet<BookInventory> BooksCollection { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK_Cart_CartId");

            entity.ToTable("Cart");

            entity.HasMany(e => e.Mappings)
        .WithOne(e => e.Carts)
        .HasForeignKey(e => e.CartId).HasConstraintName("FK_Carts_Map");
            ;
        });


        modelBuilder.Entity<BookInventory>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK_Book_BookId");

            entity.ToTable("BookInventory");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.HasMany(e => e.Mappings)
        .WithOne(e => e.Books)
        .HasForeignKey(e => e.CartId).HasConstraintName("FK_Book_Map");
            //sedding data in book invetory 
            entity.HasData(
            new BookInventory { BookId = 1, Author = "Chetan", BookName = "Hamlet", Quantity = 3, Price = 100 },
            new BookInventory { BookId = 2, Author = "Bhagat", BookName = "Hamlet1", Quantity = 4, Price = 101 },
            new BookInventory { BookId = 3, Author = "Amish", BookName = "Hamlet2", Quantity = 5, Price = 102 },
            new BookInventory { BookId = 4, Author = "Harrish", BookName = "Hamlet3", Quantity = 2, Price = 103 },
            new BookInventory { BookId = 5, Author = "Martin", BookName = "Hamlet4", Quantity = 1, Price = 104 });
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_UserProfile_UserId");

            entity.ToTable("UserProfile");

            entity.Property(e => e.UserId).HasMaxLength(128);
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasDefaultValue("Guest");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);

            //sedding data in userprofile 
            entity.HasData(
            new UserProfile { UserId = 1,  DisplayName = "Guest", FirstName = "Guest", Email = "abc@bca.com" },
            new UserProfile { UserId = 2, DisplayName = "Robert", FirstName = "Robert", LastName = "Martin", Email = "robert@martin.com" });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

