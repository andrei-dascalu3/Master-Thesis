using BooksApp.Core.Adapters.DataAccess;
using BooksApp.Library.Entities;
using BooksApp.Library.Ports.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Library.Adapters.DataAccess;

public class LibraryRepository(LibraryDbContext dbContext) : BaseRepository<LibraryDbContext, Book>(dbContext), ILibraryRepository
{
}

public class LibraryDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd();
    }
}