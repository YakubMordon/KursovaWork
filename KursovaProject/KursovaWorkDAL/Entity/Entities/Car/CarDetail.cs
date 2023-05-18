using System.ComponentModel.DataAnnotations;

namespace KursovaWorkDAL.Entity.Entities.Car
{
    /// <summary>
    /// Деталі автомобіля.
    /// </summary>
    public class CarDetail
    {
        /// <summary>
        /// Ідентифікатор деталі автомобіля.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ідентифікатор автомобіля.
        /// </summary>
        [Required]
        public int CarId { get; set; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        /// <summary>
        /// Колір автомобіля.
        /// </summary>
        [StringLength(50)]
        public string Color { get; set; }

        /// <summary>
        /// Тип коробки передач автомобіля.
        /// </summary>
        [StringLength(50)]
        public string Transmission { get; set; }

        /// <summary>
        /// Тип палива автомобіля.
        /// </summary>
        [StringLength(50)]
        public string FuelType { get; set; }

        /// <summary>
        /// Автомобіль, до якого належать деталі.
        /// </summary>
        public virtual CarInfo Car { get; set; }

#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    }
}
