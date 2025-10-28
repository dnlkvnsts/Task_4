using Microsoft.AspNetCore.Mvc;
using Task_4.DTOs;
using Task_4.Extensions;
using Task_4.Models;
using Task_4.Repositories;
using static System.Reflection.Metadata.BlobBuilder;

namespace Task_4.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapperService _mapper;  

        public BookService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IMapperService mapper) 
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public ActionResult<IEnumerable<BookDTO>> GetAllBooks()
        {
            var books = _bookRepository.GetAll();
         
            return new OkObjectResult(books.Select(_mapper.MapBookToDTO));
        }

        public ActionResult<BookDTO> GetBookById(int id)
        {
            var book = _bookRepository.GetById(id);

            if (book == null)
            {
                return ResultExtension.EntityNotFound<BookDTO>(id, "Book");
            }

            return new OkObjectResult(_mapper.MapBookToDTO(book));
        }

        public ActionResult<BookDTO> CreateBook(BookCreateDTO dto)
        {
            var author = _authorRepository.GetById(dto.AuthorId);

            if (author == null)
            {
                return ResultExtension.EntityBadRequest($"Author with ID {dto.AuthorId} does not exist");
            }

            var book = new Book
            {
                Title = dto.Title,
                PublishedYear = dto.PublishedYear,
                AuthorId = dto.AuthorId
            };

            _bookRepository.Create(book);

     
            return new CreatedResult(
                $"/api/books/{book.Id}",
                _mapper.MapBookToDTO(book)
            );
        }

        public ActionResult<BookDTO> UpdateBook(int id, BookCreateDTO dto)
        {
            var book = _bookRepository.GetById(id);

            if (book == null)
            {
                return ResultExtension.EntityNotFound<BookDTO>(id, "Book");
            }

            var author = _authorRepository.GetById(dto.AuthorId);

            if (author == null)
            {
                return ResultExtension.EntityBadRequest($"Author with ID {dto.AuthorId} does not exist");
            }

            book.Title = dto.Title;
            book.PublishedYear = dto.PublishedYear;
            book.AuthorId = dto.AuthorId;

            _bookRepository.Update(book);

           
            return new OkObjectResult(_mapper.MapBookToDTO(book));
        }

        public ActionResult<BookDTO> DeleteBook(int id)
        {
            var book = _bookRepository.GetById(id);

            if (book == null)
            {
                return ResultExtension.EntityNotFound<BookDTO>(id, "Book");
            }

            _bookRepository.Delete(id);

            return new NoContentResult();
        }
    }
}
