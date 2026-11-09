using BooksApp.Core.Ports.DataAccess;
using BooksApp.Library.Entities;

namespace BooksApp.Library.Ports.DataAccess;

public interface ILibraryRepository : IBaseRepository<Book>
{
}
