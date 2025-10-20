using BooksApp.Library.Domain.Contexts.Interfaces;
using BooksApp.Library.Entities;
using BooksApp.Library.Ports.DataAccess;

namespace BooksApp.Library.Domain.Contexts;

public class OverviewContext : IOverviewContext
{
    private readonly ILibraryRepository libraryRepository;

    public OverviewContext(ILibraryRepository libraryRepository)
    {
        this.libraryRepository = libraryRepository;
    }

    public IEnumerable<Book> GetBooks()
    {
        return libraryRepository.GetBooks();
    }

    public Book GetBook(Guid id)
    {
        return libraryRepository.GetBook(id) ?? new Book();
    }

    public Book AddBook(Book book)
    {
        libraryRepository.AddBook(book);
        return book;
    }
}
