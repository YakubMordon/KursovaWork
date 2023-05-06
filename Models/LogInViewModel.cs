using KursovaWork.Entity;
using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Models
{
    public class LogInViewModel
    {
        public LogInViewModel()
        {   
        }

        [Required(ErrorMessage = "Поле email є потрібне.")]
        [EmailAddress(ErrorMessage = "Неправильна email адреса.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле паролю є потрібне.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public User ValidateUser(CarSaleContext context)
        {
            return context.Users.SingleOrDefault(u => u.Email == Email && u.Password == Password);
        }

    }

}
