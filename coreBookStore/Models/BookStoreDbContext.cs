using Microsoft.EntityFrameworkCore;
using OnlineBookStoreUser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreBookStore.Models
{
    public class BookStoreDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderBook> OrderBook { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=TRD-510;Initial Catalog=BookStore;Integrated Security=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderBook>
                (build =>
                {
                    build.HasKey(b => new { b.OrderId, b.BookId });


            });
        }
    }
}
