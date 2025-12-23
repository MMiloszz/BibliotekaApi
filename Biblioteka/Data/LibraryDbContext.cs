using Biblioteka.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Copy> Copies => Set<Copy>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author!)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Copies)
            .WithOne(c => c.Book!)
            .HasForeignKey(c => c.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Author>().Property(a => a.FirstName).IsRequired();
        modelBuilder.Entity<Author>().Property(a => a.LastName).IsRequired();
        modelBuilder.Entity<Book>().Property(b => b.Title).IsRequired();
    }
}
