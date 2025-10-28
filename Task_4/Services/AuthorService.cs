using Microsoft.AspNetCore.Mvc;
using Task_4.DTOs;
using Task_4.Models;
using Task_4.Repositories;
using Task_4.Extensions;

namespace Task_4.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapperService _mapper;

        public AuthorService(
            IAuthorRepository authorRepository,
            IBookRepository bookRepository,
            IMapperService mapper)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public ActionResult<IEnumerable<AuthorDTO>> GetAllAuthors()
        {
            var authors = _authorRepository.GetAll();

            return new OkObjectResult(authors.Select(_mapper.MapAuthorToDTO));
        }

        public ActionResult<AuthorDTO> GetAuthorById(int id)
        {
            var author = _authorRepository.GetById(id);

            if (author == null)
            {
                return ResultExtension.EntityNotFound<AuthorDTO>(id, "Author");
            }

            return new OkObjectResult(_mapper.MapAuthorToDTO(author));
        }

        public ActionResult<AuthorDTO> CreateAuthor(AuthorCreateDTO dto)
        {
            var author = new Author
            {
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth
            };

            _authorRepository.Create(author);


            return new CreatedResult(
                $"/api/authors/{author.Id}",
                _mapper.MapAuthorToDTO(author)
            );
        }

        public ActionResult<AuthorDTO> UpdateAuthor(int id, AuthorCreateDTO dto)
        {
            var author = _authorRepository.GetById(id);


            if (author == null)
            {
                return ResultExtension.EntityNotFound<AuthorDTO>(id, "Author");
            }

            author.Name = dto.Name;
            author.DateOfBirth = dto.DateOfBirth;

            _authorRepository.Update(author);


            return new OkObjectResult(_mapper.MapAuthorToDTO(author));
        }

        public ActionResult<AuthorDTO> DeleteAuthor(int id)
        {
            var author = _authorRepository.GetById(id);

            if (author == null)
            {
                return ResultExtension.EntityNotFound<AuthorDTO>(id, "Author");
            }

            var books = _bookRepository.GetBooksByAuthorId(id).ToList();

            if (books.Any())
            {
                return ResultExtension.EntityBadRequest($"Cannot delete author with {books.Count} book(s)");
            }

            _authorRepository.Delete(id);

            return new NoContentResult();
        }
    }
}
