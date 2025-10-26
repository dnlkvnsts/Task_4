using System.ComponentModel.DataAnnotations;

namespace Task_4.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title of book is necessary")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Title should be have length from 1 to 20")]
        public string Title { get; set; }

        [Required(ErrorMessage = "PublishedYear of book is necessary")]
        [Range(1000,9999, ErrorMessage = "Title should be from 1000 to 9999")]
        public int PublishedYear { get; set; }

        [Required(ErrorMessage = "ID of author is necessary")]
        [Range(1, int.MaxValue, ErrorMessage = "Author id is a positive number")]
        public int AuthorId { get; set; }

    }
}
