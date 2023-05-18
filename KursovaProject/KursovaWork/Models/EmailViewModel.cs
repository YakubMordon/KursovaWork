using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Models
{
    /// <summary>
    /// Клас для заполучення електронної пошти, під час зміни паролю
    /// </summary>
    public class EmailViewModel
    {
        /// <summary>
        /// Електронна пошта
        /// </summary>
        [Required(ErrorMessage = "Поле Електронна пошта є обов'язковим")]
        [EmailAddress(ErrorMessage = "Поле, не являється електронною поштою")]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }
    }
}
