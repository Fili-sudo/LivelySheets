using LivelySheets.CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LivelySheets.CatalogService.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<ListedBook> ListedBooks { get; set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.ListedBooks)
            .WithOne(lb => lb.User)
            .HasForeignKey(lb => lb.UserId);

        modelBuilder.Entity<Book>()
            .HasMany(u => u.ListedBooks)
            .WithOne(lb => lb.Book)
            .HasForeignKey(lb => lb.BookId);
    }
}
