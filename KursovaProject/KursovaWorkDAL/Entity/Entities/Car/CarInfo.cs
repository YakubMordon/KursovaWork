using System.ComponentModel.DataAnnotations;

namespace KursovaWorkDAL.Entity.Entities.Car
{
    /// <summary>
    /// Клас, який представляє інформацію про автомобіль.
    /// </summary>
    public class CarInfo
    {
        /// <summary>
        /// Унікальний ідентифікатор автомобіля.
        /// </summary>
        public int Id { get; set; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        
        /// <summary>
        /// Марка автомобіля.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Make { get; set; }

        /// <summary>
        /// Модель автомобіля.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        /// <summary>
        /// Рік виробництва автомобіля.
        /// </summary>
        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }

        /// <summary>
        /// Ціна автомобіля.
        /// </summary>
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Опис автомобіля.
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; }


        /// <summary>
        /// Зображення автомобіля.
        /// </summary>
        public virtual ICollection<CarImage> Images { get; set; }

        /// <summary>
        /// Деталі автомобіля.
        /// </summary>
        public virtual CarDetail Detail { get; set; }

        /// <summary>
        /// Замовлення на автомобіль.
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; }

#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    }

}
