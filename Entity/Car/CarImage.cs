using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Entity.Car
{
    public class CarImage
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public string ImageUrl { get; set; }

        public virtual CarInfo Car { get; set; }

#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    }
}
