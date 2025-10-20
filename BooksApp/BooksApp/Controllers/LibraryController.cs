using BooksApp.Library.Ports.Primary.Interfaces;
using BooksApp.Library.Ports.Primary.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryController : ControllerBase
{
    private readonly IOverviewManager overviewManager;

    public LibraryController(IOverviewManager overviewManager)
    {
        this.overviewManager = overviewManager;
    }

    [HttpGet("books")]
    public IActionResult GetBooks()
    {
        var books = this.overviewManager.GetBooks();
        if (books == null || !books.Any())
        {
            return NotFound("No books found.");
        }

        return Ok(books);
    }

    [HttpGet("books/{id}")]
    public IActionResult GetBook(Guid id)
    {
        var book = this.overviewManager.GetBook(id);
        if (book == null)
        {
            return NotFound($"Book with ID {id} not found.");
        }

        return Ok(book);
    }

    [HttpPost("books")]
    public IActionResult InsertBook([FromBody] Book book)
    {
        if (book == null)
        {
            return BadRequest("Book data is required.");
        }

        var addedBook = this.overviewManager.AddBook(book);
        if (addedBook == null)
        {
            return StatusCode(500, "Failed to add book.");
        }

        return CreatedAtAction(nameof(GetBook), new { id = addedBook.Id }, addedBook);
    }
}
