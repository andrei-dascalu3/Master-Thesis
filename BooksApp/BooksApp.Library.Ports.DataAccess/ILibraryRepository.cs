using BooksApp.Library.Entities;

namespace BooksApp.Library.Ports.DataAccess;

public interface ILibraryRepository
{
    List<Book> GetBooks();
    Book GetBook(Guid id);
    Book AddBook(Book book);
}
