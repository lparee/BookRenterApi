using Carts.Core.Entities;
using Microsoft.EntityFrameworkCore;

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
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Initial Catalog=BookRenterDB;Integrated Security=SSPI; MultipleActiveResultSets=true;TrustServerCertificate=True;");


    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<BookInventory> BooksCollection { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK_Cart_CartId");

            entity.ToTable("Cart");

            entity.HasOne(d => d.Book).WithMany(p => p.Carts)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK_Cart_Book");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Cart_UserProfile");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_Category_CategoryId");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<BookInventory>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK_Book_BookId");

            entity.ToTable("BookInventory");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BookName).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Book_Category");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_UserProfile_UserId");

            entity.ToTable("UserProfile");

            entity.Property(e => e.LoginId).HasMaxLength(128);
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasDefaultValue("Guest");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

