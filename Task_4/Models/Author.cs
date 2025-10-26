
using System.ComponentModel.DataAnnotations;

namespace Task_4.Models
{
    public class Author
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Name of author is necessary")]
        [StringLength(20, MinimumLength = 1,ErrorMessage = "Name should be have length from 1 to 20") ]
        public string Name { get; set; }

        [Required(ErrorMessage = "Data is necessary")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}
