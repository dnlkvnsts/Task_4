using Task_4.Models;

namespace Task_4.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {




        public IEnumerable<Author> GetAll()
        {

            return Database.Authors;
        }

        public Author GetById(int id)
        {

            return Database.Authors.FirstOrDefault(a => a.Id == id);
        }

        public void Create(Author author)
        {


            author.Id = Database.Authors.Any()
                ? Database.Authors.Max(a => a.Id) + 1
                : 1;

            Database.Authors.Add(author);


        }

        public void Update(Author author)
        {


            var existing = GetById(author.Id);
            if (existing != null)
            {
                existing.Name = author.Name;
                existing.DateOfBirth = author.DateOfBirth;


            }
        }

        public void Delete(int id)
        {


            var author = GetById(id);
            if (author != null)
            {
                Database.Authors.Remove(author);


            }
        }
    }
}

