using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_4.Models;

namespace Task_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(Database.books);
        }

        [HttpGet("{id}")]

        public ActionResult<Book> GetBookById(int id)
        {
            var book = Database.books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound($"Don't exist book with id {id}");
            }

            return Ok(book);
        }

        [HttpPost]

        public ActionResult<Book> CreateBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Data of book can't be empty");
            }

            var authorExists = Database.authors.Any(a => a.Id == book.AuthorId);

            if (!authorExists)
            {
                return BadRequest($"Don't exist author with id {book.AuthorId}");
            }

            book.Id = Database.books.Any() ? Database.books.Max(b => b.Id) + 1 : 1;

            Database.books.Add(book);

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public ActionResult<Book> UpdateBook(int id, [FromBody] Book updatedBook)
        {
            if (updatedBook == null)
            {
                return BadRequest("Data cant't be empty");
            }

            var book = Database.books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound($"Don't exist book with id {id}");
            }

            var authorExists = Database.authors.Any(a => a.Id == updatedBook.AuthorId);

            if (!authorExists)
            {
                return BadRequest($"Don't exist author with id {updatedBook.AuthorId}");
            }

            book.Title = updatedBook.Title;
            book.PublishedYear = updatedBook.PublishedYear;
            book.AuthorId = updatedBook.AuthorId;


            return Ok(book);
        }

        [HttpDelete("{id}")]

        public ActionResult DeleteBook(int id)
        {
            var book = Database.books.FirstOrDefault(b => b.Id == id);


            if (book == null)
            {
                return NotFound($"Don't exist book with id {id}");
            }

            Database.books.Remove(book);

            return NoContent();
        }
    }
}
