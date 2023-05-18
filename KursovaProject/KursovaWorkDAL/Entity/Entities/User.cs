using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KursovaWorkDAL.Entity.Entities
{
    /// <summary>
    /// Клас, що представляє користувача.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Унікальний ідентифікатор користувача.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ім'я користувача.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Прізвище користувача.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Електронна пошта користувача.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Пароль користувача.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// Підтвердження пароля користувача.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Дата і час створення користувача.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Колекція замовлень, пов'язаних з користувачем.
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; }

        /// <summary>
        /// Кредитна картка користувача (зв'язок один-до-одного).
        /// </summary>
        [ForeignKey("UserId")]
        public virtual Card CreditCard { get; set; }
    }
}
