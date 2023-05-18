using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Models
{
    /// <summary>
    /// Клас для получення даних входу, які ввів користувач
    /// </summary>
    public class LogInViewModel
    {
        /// <summary>
        /// Електронна пошта
        /// </summary>
        [Required(ErrorMessage = "Поле Електронна пошта є потрібне.")]
        [EmailAddress(ErrorMessage = "Неправильна Електронна пошта.")]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Поле паролю є потрібне.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

    }

}
