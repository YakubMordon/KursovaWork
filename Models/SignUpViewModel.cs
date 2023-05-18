using System.ComponentModel.DataAnnotations;
using KursovaWorkDAL.Entity.Entities;

namespace KursovaWork.Models
{
    /// <summary>
    /// Клас для получення даних реєстрації, які ввів користувач
    /// </summary>
    public class SignUpViewModel
    {
        /// <summary>
        /// Ім'я
        /// </summary>
        [Required(ErrorMessage = "Поле Ім\'я є обов\'язковим")]
        [StringLength(50)]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        /// <summary>
        /// Прізвище
        /// </summary>
        [Required(ErrorMessage = "Поле Прізвище є обов'язковим")]
        [StringLength(50)]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        /// <summary>
        /// Електронна пошта
        /// </summary>
        [Required(ErrorMessage = "Поле Електронна пошта є обов'язковим")]
        [EmailAddress(ErrorMessage = "Поле, не являється електронною поштою")]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Поле Пароль є обов'язковим")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль повинен містити мінімум 6 символів")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Підтвердження пароля
        /// </summary>
        [Required(ErrorMessage = "Поле Підтвердження пароля є обов'язковим")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Поле Підтвердження паролю та Пароль повинні бути одинакові")]
        [Display(Name = "Підтвердження пароля")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Метод перетворення об'єкту класу SignUpViewModel у об'єкт класу User
        /// </summary>
        /// <returns>Об'єкт класу User</returns>
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
