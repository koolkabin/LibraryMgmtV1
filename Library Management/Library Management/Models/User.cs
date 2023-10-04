using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Faculty { get; set; }
        public int Status { get; set; }
        public EnumUserType UserType { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; }

        public ICollection<LentBook> LentBook { get; set; }
        public ICollection<ReturnBook> ReturnBook { get; set; }
        public ICollection<RequestBook> RequestBook { get; set; }

    }
    public enum EnumUserType
    {
        Admin = 1,
        Student = 2
    }
}


