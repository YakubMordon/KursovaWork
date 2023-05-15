using System.ComponentModel.DataAnnotations;
using KursovaWork.Entity.Entities.Car;

namespace KursovaWork.Entity.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        [Required]
        public DateTime OrderDate { get; set; }

        public virtual CarInfo Car { get; set; }

        public virtual User User { get; set; }

        public virtual ConfiguratorOptions? ConfiguratorOptions { get; set; }

#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    }
}
