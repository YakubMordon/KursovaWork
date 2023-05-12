using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Models
{
    public class CreditCardViewModel
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
        [StringLength(2, MinimumLength = 2)]
        public string ExpirationMonth { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string ExpirationYear { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string CVV { get; set; }
    }
}
