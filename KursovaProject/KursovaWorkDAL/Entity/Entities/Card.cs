using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KursovaWorkDAL.Entity.Entities
{
    /// <summary>
    /// Представляє сутність кредитної картки.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Ідентифікатор користувача.
        /// </summary>
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        /// <summary>
        /// Номер кредитної картки.
        /// </summary>
        [Required]
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public string CardNumber { get; set; }

        /// <summary>
        /// Ім'я власника кредитної картки.
        /// </summary>
        [Required]
        public string CardHolderName { get; set; }

        /// <summary>
        /// Місяць закінчення терміну дії кредитної картки.
        /// </summary>
        [Required]
        public string ExpirationMonth { get; set; }

        /// <summary>
        /// Отримує або задає рік закінчення терміну дії кредитної картки.
        /// </summary>
        [Required]
        public string ExpirationYear { get; set; }

        /// <summary>
        /// CVV-код кредитної картки.
        /// </summary>
        [Required]
        public string CVV { get; set; }

        /// <summary>
        /// Зв'язок з користувачем.
        /// </summary>
        public virtual User User { get; set; }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    }
}
