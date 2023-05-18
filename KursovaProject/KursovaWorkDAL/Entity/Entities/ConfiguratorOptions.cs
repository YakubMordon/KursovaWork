using System.ComponentModel.DataAnnotations;

namespace KursovaWorkDAL.Entity.Entities
{
    /// <summary>
    /// Клас, що представляє параметри конфігуратора.
    /// </summary>
    public class ConfiguratorOptions
    {
        /// <summary>
        /// Ідентифікатор параметрів конфігуратора.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ідентифікатор замовлення, до якого належать параметри конфігуратора.
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// Колір автомобіля.
        /// </summary>
        [StringLength(50)]
        public string? Color { get; set; }

        /// <summary>
        /// Тип трансмісії автомобіля.
        /// </summary>
        [StringLength(50)]
        public string? Transmission { get; set; }

        /// <summary>
        /// Тип палива автомобіля.
        /// </summary>
        [StringLength(50)]
        public string? FuelType { get; set; }

        /// <summary>
        /// Зв'язок з замовленням.
        /// </summary>
        public virtual Order? Order { get; set; }
    }
}
