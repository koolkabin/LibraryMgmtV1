using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Library_Management.Models
{
    public class Book
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

        [ForeignKey("Publication")]
        public int PublicationID { get; set; }
        public virtual Publication Publication { get; set; }

        
        [ForeignKey("BookLevel")]
        public int LevelId { get; set; }
        public virtual BookLevel BookLevel { get; set; }

        [ForeignKey("BookAuthor")]
        public int AuthorId {  get; set; }
        public virtual BookAuthor BookAuthor { get; set; }

        [ForeignKey("BookCategory")]
        public int CategoryId { get; set; }
        public virtual BookCategory BookCategory { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<RequestBook> RequestBook { get; set; }
    }
    public class VMBook
    {
        public IList<Book> TotalLibraryBookList { get; set; } = new List<Book>();
        public IList<RequestBook> CurrentRequestBookList { get; set; } = new List<RequestBook>();
    }
}
