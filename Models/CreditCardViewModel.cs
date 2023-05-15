using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Models
{
    public class CreditCardViewModel
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        
        [Required(ErrorMessage = "Поле Номер карти є обов'язковим")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Довжина номеру карти повинна бути мінімум 16")]
        [RegularExpression(@"^(?:\d{16})$", ErrorMessage = "Неправильний номер карти")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Поле Ім'я та прізвище є обов'язковим")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Поле Термін дії(місяць) є обов'язковим")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Довжина Терміну дії(місяць) повинна бути мінімум 2")]
        public string ExpirationMonth { get; set; }

        [Required(ErrorMessage = "Поле Термін дії(рік) є обов'язковим")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Довжина Терміну дії(рік) повинна бути мінімум 2")]
        public string ExpirationYear { get; set; }

        [Required(ErrorMessage = "Поле CVV є обов'язковим")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Довжина CVV повинна бути мінімум 3")]
        public string CVV { get; set; }
    }
}
