using LibraryAPI.Core.Exceptions;
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
            var books = _bookService.GetAllBooks() ?? throw new NotFoundException("Book records not found.");

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBookDetails(id) ?? throw new NotFoundException("Book record not found.");

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Input parameter is not valid.");
            }

            await _bookService.CreateBook(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Input parameter is not valid.");
            }

            if (id != book.Id)
            {
                throw new BadRequestException("ID of book is different in body and parameter.");
            }

            await _bookService.UpdateBook(book);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Input parameter is not valid.");
            }

            await _bookService.DeleteBook(id);
            return Ok();
        }
    }
}