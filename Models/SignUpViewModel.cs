using System.ComponentModel.DataAnnotations;
using KursovaWork.Entity.Entities;

namespace KursovaWork.Models
{
    public class SignUpViewModel
    {
        public SignUpViewModel() { }

        [Required]
        [StringLength(50)]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Підтвердження пароля")]
        public string ConfirmPassword { get; set; }

        public User ToUser()
        {
            return new User
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Password = this.Password,
                ConfirmPassword = this.ConfirmPassword
            };
        }

    }
}
