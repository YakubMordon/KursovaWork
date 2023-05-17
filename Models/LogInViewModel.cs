using KursovaWork.Entity;
using KursovaWork.Entity.Entities;
using KursovaWork.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Метод валідації чи є такий користувач у базі даних
        /// </summary>
        /// <param name="context">Контекс бази даних, для заполучення користувача</param>
        /// <returns>Повертає користувача, якщо він є у базі даних, якщо немає то null</returns>
        public User ValidateUser(CarSaleContext context)
        {
            List<User> users = context.Users.ToList();
            string hashPassword = Encrypter.HashPassword(Password);
            foreach(User user in users)
            {
                if (user.Email == Email && user.Password == hashPassword)
                {
                    return user;
                }
            }
            
            return null;
        }


    }

}
