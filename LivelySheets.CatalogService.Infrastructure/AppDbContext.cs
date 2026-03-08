using LivelySheets.CatalogService.Domain.Entities;
using LivelySheets.CatalogService.Domain.Entities.Messages;
using Microsoft.EntityFrameworkCore;

namespace LivelySheets.CatalogService.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<ListedBook> ListedBooks { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.ListedBooks)
            .WithOne(lb => lb.User)
            .HasForeignKey(lb => lb.UserId)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .HasMany(u => u.ListedBooks)
            .WithOne(lb => lb.Book)
            .HasForeignKey(lb => lb.BookId)
            .IsRequired();

        modelBuilder.Entity<OutboxMessage>()
            .Property(m => m.InboxMessageId)
            .IsRequired(false);

        modelBuilder.Entity<OutboxMessage>()
            .Property(m => m.UpdatedOn)
            .IsRequired(false);
    }
}
