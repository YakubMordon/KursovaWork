using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KursovaWork.Entity.Entities
{
    public class Card
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public string CardNumber { get; set; }

        [Required]
        public string CardHolderName { get; set; }

        [Required]
        public string ExpirationMonth { get; set; }

        [Required]
        public string ExpirationYear { get; set; }

        [Required]
        public string CVV { get; set; }

        public virtual User User { get; set; }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    }
}
