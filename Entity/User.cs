using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KursovaWork.Entity
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        // One-To-Many Relationship
        public virtual ICollection<Order> Orders { get; set; }

        // One-To-One Relationship
        [ForeignKey("UserId")]
        public virtual Card CreditCard { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
