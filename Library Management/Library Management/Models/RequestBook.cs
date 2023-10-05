using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Library_Management.Models
{
    public class RequestBook
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Books")]
        public int BookId { get; set; }
        public virtual Books Books { get; set; }
        public virtual RequestCancelledLog RequestCancelledLog { get; set; }
        public EnumRequestStatus RequestStatus { get; set; } = EnumRequestStatus.Pending;

        public bool CanAccept => RequestStatus != EnumRequestStatus.Pending;
        public bool CanReject => RequestStatus != EnumRequestStatus.Pending;
        public bool CanCancel => RequestStatus != EnumRequestStatus.Pending;
    }

    public enum EnumRequestStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2,
        Cancelled = 3
    }
}
