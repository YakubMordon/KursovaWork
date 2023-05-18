using System.ComponentModel.DataAnnotations;
using KursovaWorkDAL.Entity.Entities.Car;

namespace KursovaWorkDAL.Entity.Entities
{
    /// <summary>
    /// Представляє замовлення.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Ідентифікатор замовлення.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ідентифікатор автомобіля.
        /// </summary>
        [Required]
        public int CarId { get; set; }

        /// <summary>
        /// Ідентифікатор користувача.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Ціна замовлення.
        /// </summary>
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        /// <summary>
        /// Дата замовлення.
        /// </summary>
        [Required]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Автомобіль, пов'язаний із замовленням.
        /// </summary>
        public virtual CarInfo Car { get; set; }

        /// <summary>
        /// Користувач, пов'язаний із замовленням.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Опції конфігуратора, пов'язані із замовленням.
        /// </summary>
        public virtual ConfiguratorOptions? ConfiguratorOptions { get; set; }

#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    }
}
