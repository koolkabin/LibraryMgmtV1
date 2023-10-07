using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Library_Management.Models
{
    public class RequestCancelledLog
    {
        [Key]
        [ForeignKey("RequestBook")]
        public int RequestBookID { get; set; }
        public virtual RequestBook RequestBook { get; set; }

        public DateTime CancelledDate { get; set; }
        public string Remarks { get; set; }
    }
}
