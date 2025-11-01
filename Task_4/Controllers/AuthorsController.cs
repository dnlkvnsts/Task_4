using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_4.DTOs;
using Task_4.Models;
using Task_4.Services;

namespace Task_4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(
            IAuthorService service)
        {
            _service = service;
        }


        [HttpGet]

        public ActionResult<IEnumerable<AuthorDTO>> GetAll()
        {
            return _service.GetAllAuthors();
        }


        [HttpGet("{id}")]

        public ActionResult<AuthorDTO> GetById(int id)
        {

            return _service.GetAuthorById(id);
        }


        [HttpPost]

        public async Task<ActionResult<AuthorDTO>> Create([FromBody] AuthorCreateDTO dto)
        {
            var authorResult = _service.CreateAuthor(dto);


            var author = authorResult.Value;


            if (author == null)
            {

                return authorResult.Result;
            }

            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);

        }


        [HttpPut("{id}")]

        public async Task<ActionResult<AuthorDTO>> Update(int id, [FromBody] AuthorCreateDTO dto)
        {
            return _service.UpdateAuthor(id, dto);
        }


        [HttpDelete("{id}")]

        public ActionResult<AuthorDTO> Delete(int id)
        {

            return _service.DeleteAuthor(id);
        }
    }
}