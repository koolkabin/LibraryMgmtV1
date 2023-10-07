using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class ReturnBook
    {
        [Key]
        [ForeignKey("RequestBook")]
        public int RequestBookId { get; set; }
        public virtual RequestBook RequestBook { get; set; }
        
        [Required]
        public DateTime returnedDate { get; set; }
        public string Remarks { get; set; }

    }
}
