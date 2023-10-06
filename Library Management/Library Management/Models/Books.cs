using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Library_Management.Models
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public string Publication { get; set; }
        [Required]
        public string Level { get; set; }
        [ForeignKey("BookAuthor")]
        public int AuthorId {  get; set; }
        public BookAuthor BookAuthor { get; set; }

        [ForeignKey("BookCategory")]
        public int CategoryId { get; set; }
        public BookCategory BookCategory { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; }

        public ICollection<LentBook> LentBook { get; set; }
        public ICollection<ReturnBook> ReturnBook { get; set; }
        public ICollection<RequestBook> RequestBook { get; set; }
    }
    public class VMBook
    {
        public IList<Books> TotalLibraryBookList { get; set; } = new List<Books>();
        public IList<RequestBook> CurrentRequestBookList { get; set; } = new List<RequestBook>();
    }
}
