using Task_4.Models;

namespace Task_4.Repositories
{
    public class BookRepository : IBookRepository
    {


        public IEnumerable<Book> GetAll()
        {

            return Database.Books;
        }

        public Book GetById(int id)
        {

            return Database.Books.FirstOrDefault(b => b.Id == id);
        }

        public void Create(Book book)
        {


            book.Id = Database.Books.Any()
                ? Database.Books.Max(b => b.Id) + 1
                : 1;

            Database.Books.Add(book);


        }

        public void Update(Book book)
        {


            var existing = GetById(book.Id);
            if (existing != null)
            {
                existing.Title = book.Title;
                existing.PublishedYear = book.PublishedYear;
                existing.AuthorId = book.AuthorId;


            }
        }

        public void Delete(int id)
        {

            var book = GetById(id);
            if (book != null)
            {
                Database.Books.Remove(book);


            }
        }

        public IEnumerable<Book> GetBooksByAuthorId(int authorId)
        {

            return Database.Books.Where(b => b.AuthorId == authorId);
        }
    }
}