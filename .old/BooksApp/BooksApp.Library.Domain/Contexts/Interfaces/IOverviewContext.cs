using BooksApp.Library.Entities;

namespace BooksApp.Library.Domain.Contexts.Interfaces;

public interface IOverviewContext
{
    public IEnumerable<Book> GetBooks();
    public Book GetBook(Guid id);
    public Book AddBook(Book book);
}
