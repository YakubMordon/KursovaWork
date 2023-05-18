using System.ComponentModel.DataAnnotations;

namespace KursovaWorkDAL.Entity.Entities.Car
{
    /// <summary>
    /// Клас, що представляє зображення автомобіля.
    /// </summary>
    public class CarImage
    {
        /// <summary>
        /// Ідентифікатор зображення автомобіля.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ідентифікатор автомобіля, до якого належить зображення.
        /// </summary>
        [Required]
        public int CarId { get; set; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        /// <summary>
        /// URL зображення автомобіля.
        /// </summary>
        [Required]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Об'єкт, що представляє автомобіль, до якого належить зображення.
        /// </summary>
        public virtual CarInfo Car { get; set; }

#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    }
}
