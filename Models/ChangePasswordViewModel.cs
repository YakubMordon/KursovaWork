using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Models
{
    /// <summary>
    /// Клас для зміни паролю на новий
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Новий пароль
        /// </summary>

        [Required(ErrorMessage = "Поле Пароль є обов'язковим")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль повинен містити мінімум 6 символів")]
        [Display(Name = "Новий пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Підтвердження нового паролю
        /// </summary>
        
        [Required(ErrorMessage = "Поле Підтвердження нового пароля є обов'язковим")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Поле Підтвердження нового пароля та Новий пароль повинні бути одинакові")]
        [Display(Name = "Підтвердження нового пароля")]
        public string ConfirmPassword { get; set; }
    }
}
