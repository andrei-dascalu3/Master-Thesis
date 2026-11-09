using BooksApp.Library.Entities;
using Models = BooksApp.Library.Ports.Primary.Models;

namespace BooksApp.Library.Adapters.Primary.Mappers;

internal static class ModelMapper
{
    public static Models.Book ToModel(this Book book)
    {
        return new Models.Book
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Description = book.Description,
            PublishedDate = book.PublishedDate,
            ISBN = book.ISBN
        };
    }
}
