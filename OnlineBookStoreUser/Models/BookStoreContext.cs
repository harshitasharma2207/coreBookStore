using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineBookStoreUser.Models
{
    public partial class BookStoreContext : DbContext
    {
        public BookStoreContext()
        {
        }

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<BookCategories> BookCategories { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderBook> OrderBook { get; set; }
        public virtual DbSet<Publications> Publications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=TRD-510;Database=BookStore;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>(entity =>
            {
                entity.HasKey(e => e.AuthorId);
            });

            modelBuilder.Entity<BookCategories>(entity =>
            {
                entity.HasKey(e => e.BookCategoryId);
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.HasKey(e => e.BookId);

                entity.HasIndex(e => e.AuthorId);

                entity.HasIndex(e => e.BookCategoryId);

                entity.HasIndex(e => e.PublicationId);

                entity.HasOne(d => d.Authors)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId);

                entity.HasOne(d => d.BookCategory)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.BookCategoryId);

                entity.HasOne(d => d.Publication)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublicationId);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.CustomerId);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<OrderBook>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.BookId });

                entity.HasIndex(e => new { e.BookId, e.OrderId })
                    .HasName("AK_OrderBook_BookId_OrderId")
                    .IsUnique();

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OrderBook)
                    .HasForeignKey(d => d.BookId);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderBook)
                    .HasForeignKey(d => d.OrderId);
            });

            modelBuilder.Entity<Publications>(entity =>
            {
                entity.HasKey(e => e.PublicationId);
            });
        }
    }
}
