using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Models
{
    public class EmailViewModel
    {
        [Required(ErrorMessage = "Поле Електронна пошта є обов'язковим")]
        [EmailAddress(ErrorMessage = "Поле, не являється електронною поштою")]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }
    }
}
