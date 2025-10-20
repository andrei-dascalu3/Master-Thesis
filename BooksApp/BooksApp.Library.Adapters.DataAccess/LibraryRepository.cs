using BooksApp.Library.Entities;
using BooksApp.Library.Ports.DataAccess;

namespace BooksApp.Library.Adapters.DataAccess;

public class LibraryRepository : ILibraryRepository
{
    private readonly List<Book> books =
    [
        new() {
            Id = Guid.NewGuid(),
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald",
            Description = "A novel set in the Roaring Twenties, exploring themes of decadence and excess.",
            PublishedDate = new DateTime(1925, 4, 10),
            ISBN = "9780743273565"
        },
        new() {
            Id = Guid.NewGuid(),
            Title = "To Kill a Mockingbird",
            Author = "Harper Lee",
            Description = "A novel about the serious issues",
        },
        new() {
            Id = Guid.NewGuid(),
            Title = "1984",
            Author = "George Orwell",
            Description = "A dystopian novel set in a totalitarian society under constant surveillance.",
            PublishedDate = new DateTime(1949, 6, 8),
            ISBN = "9780451524935"
        },
        new() {
            Id = Guid.NewGuid(),
            Title = "Pride and Prejudice",
            Author = "Jane Austen",
            Description = "A classic novel that explores themes of love, social class, and reputation.",
            PublishedDate = new DateTime(1813, 1, 28),
            ISBN = "9780141439518"
        },
        new() {
            Id = Guid.NewGuid(),
            Title = "The Catcher in the Rye",
            Author = "J.D. Salinger",
            Description = "A novel that delves into the challenges of teenage angst and alienation.",
            PublishedDate = new DateTime(1951, 7, 16),
            ISBN = "9780316769488"
        },
    ];

    public Book GetBook(Guid id)
    {
        return books.FirstOrDefault(b => b.Id == id) ?? new Book();
    }

    public List<Book> GetBooks()
    {
        return books;
    }

    public Book AddBook(Book book)
    {
        book.Id = Guid.NewGuid();
        books.Add(book);
        return book;
    }
}
