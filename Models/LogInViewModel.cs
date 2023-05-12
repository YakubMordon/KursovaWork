using KursovaWork.Entity;
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

        [Required(ErrorMessage = "Поле email є потрібне.")]
        [EmailAddress(ErrorMessage = "Неправильна email адреса.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле паролю є потрібне.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public User ValidateUser(CarSaleContext context)
        {
            var arr = context.Users.ToList();
            for (int i = 0; i < arr.Count(); i++)
            {
                Console.WriteLine( arr[i].Password);
            }
            return context.Users.SingleOrDefault(u => u.Email == Email && u.Password == Password);
        }


    }

}
