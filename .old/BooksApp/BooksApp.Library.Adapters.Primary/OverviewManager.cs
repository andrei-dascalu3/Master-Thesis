using BooksApp.Library.Adapters.Primary.Mappers;
using BooksApp.Library.Domain.Contexts;
using BooksApp.Library.Domain.Contexts.Interfaces;
using BooksApp.Library.Ports.Primary.Interfaces;
using BooksApp.Library.Ports.Primary.Models;

namespace BooksApp.Library.Adapters.Primary;

public class OverviewManager : IOverviewManager
{
    private readonly IOverviewContext overviewContext;

    public OverviewManager(IOverviewContext overviewContext)
    {
        this.overviewContext = overviewContext;
    }

    public IEnumerable<Book> GetBooks()
    {
        return this.overviewContext.GetBooks().Select(ModelMapper.ToModel);
    }

    public Book GetBook(Guid id)
    {
        return this.overviewContext.GetBook(id).ToModel();
    }

    public Book AddBook(Book book)
    {
        return this.overviewContext.AddBook(book.ToEntity()).ToModel();
    }
}
