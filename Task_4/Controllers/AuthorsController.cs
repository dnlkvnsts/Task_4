using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_4.Models;

namespace Task_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAllAuthors()
        {
            return Ok(Database.authors);
        }

        [HttpGet("{id}")]

        public ActionResult<Author> GetAuthorById(int id)
        {
            var author = Database.authors.FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return NotFound($"Don't exist author with id {id}");
            }

            return Ok(author);
        }

        [HttpPost]

        public ActionResult<Author> CreateAuthor([FromBody] Author author)
        {
            if(author == null)
            {
                return BadRequest("Data of author can't be empty");
            }

            author.Id = Database.authors.Any() ? Database.authors.Max(i => i.Id) + 1 : 1;

            Database.authors.Add(author);

            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public ActionResult<Author> UpdateAuthor (int id, [FromBody] Author updatedAuthor)
        {
            if (updatedAuthor == null)
            {
                return BadRequest("Data cant't be empty");
            }

            var author = Database.authors.FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return NotFound($"Don't exist author with id {id}");
            }


            author.Name = updatedAuthor.Name;
            author.DateOfBirth = updatedAuthor.DateOfBirth;

            return Ok(author);
        }

        [HttpDelete("{id}")]

        public ActionResult DeleteAuthor(int id)
        {
            var author = Database.authors.FirstOrDefault(a => a.Id == id);


            if (author == null)
            {
                return NotFound($"Don't exist author with id {id}");
            }

            Database.authors.Remove(author);

            return NoContent();
        }
    }
}
