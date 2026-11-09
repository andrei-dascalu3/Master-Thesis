using BooksApp.Library.Ports.Primary.Models;

namespace BooksApp.Library.Adapters.Primary.Mappers;

internal static class EntityMapper
{
    public static Entities.Book ToEntity(this Book model)
    {
        return new Entities.Book
        {
            Id = model.Id,
            Title = model.Title,
            Author = model.Author,
            PublishedDate = model.PublishedDate
        };
    }
}
