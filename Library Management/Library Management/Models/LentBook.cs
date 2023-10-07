using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class LentBook
    {
        [Key, ForeignKey("RequestBook")]
        public int RequestBookId { get; set; }
        public virtual RequestBook RequestBook { get; set; }
        [Required]
        public DateTime lentDate { get; set; }

        [Required]
        public DateTime ExpectedDateToReturn => lentDate.AddDays(14);

        [NotMapped]
        public int RemDays => (ExpectedDateToReturn - DateTime.Now).Days;
    }
}
