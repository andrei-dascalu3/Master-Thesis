using BooksApp.Library.Ports.Primary.Models;

namespace BooksApp.Library.Ports.Primary.Interfaces;

public interface IOverviewManager
{
    public IEnumerable<Book> GetBooks();

    public Book GetBook(Guid id);

    public Book AddBook(Book book);
}
