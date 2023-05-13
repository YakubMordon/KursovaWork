using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Entity.Entities.Car
{
    public class CarInfo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public string Make { get; set; }
        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // One-To-Many Relationship
        public virtual ICollection<CarImage> Images { get; set; }

        // One-To-One Relationship
        public virtual CarDetail Detail { get; set; }

        // One-To-Many Relationship
        public virtual ICollection<Order> Orders { get; set; }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    }

}
