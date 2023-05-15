using KursovaWork.Entity;
using KursovaWork.Entity.Entities;
using KursovaWork.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Models
{
    public class LogInViewModel
    {
        public LogInViewModel()
        {   
        }

        [Required(ErrorMessage = "Поле Електронна пошта є потрібне.")]
        [EmailAddress(ErrorMessage = "Неправильна Електронна пошта.")]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле паролю є потрібне.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public User ValidateUser(CarSaleContext context)
        {
            return context.Users.SingleOrDefault(u => u.Email == Email && u.Password == Password);
        }


    }

}
