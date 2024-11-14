using LibraryAPI.Core.IServices;
using LibraryAPI.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Api.Controllers
{
    [ApiController]
    [Route("books")]
    public class BooksController(IBookService bookService) : ControllerBase
    {
        private readonly IBookService _bookService = bookService;

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.GetAllBooks();
            if (books == null)
            {
                return StatusCode(500, new { message = $"An unexpected error occurred." });
            }

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            try
            {
                var book = await _bookService.GetBookDetails(id);
                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An unexpected error occurred: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _bookService.CreateBook(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != book.Id)
                return BadRequest();

            await _bookService.UpdateBook(book);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);
            return Ok();
        }
    }
}