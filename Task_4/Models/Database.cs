namespace Task_4.Models
{
    public static class Database
    {
        public static List<Author> authors { get; } = new List<Author>()
        {
            new Author{ Id = 1, Name ="Nastya", DateOfBirth=new DateTime(1988,12,10)},
            new Author{ Id = 2, Name ="Kira", DateOfBirth=new DateTime(1966,10,09)}
        };

        public static List<Book> books { get; } = new List<Book>()
        {
           new Book{ Id = 1, Title ="Sea", PublishedYear = 1987, AuthorId = 1},
           new Book{ Id = 2, Title ="Dog", PublishedYear = 1966, AuthorId = 2}
        };

    }
}
