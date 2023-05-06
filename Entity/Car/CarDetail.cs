using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Entity.Car
{
    public class CarDetail
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [StringLength(50)]
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public string Color { get; set; }

        [StringLength(50)]
        public string Transmission { get; set; }

        [StringLength(50)]
        public string FuelType { get; set; }

        // One-To-One Relationship
        public virtual CarInfo Car { get; set; }

#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    }
}
