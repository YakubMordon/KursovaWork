using KursovaWork.Entity;
using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Models
{
    public class CreditCardViewModel
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        [Required]
        [StringLength(16, MinimumLength = 16)]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Please enter a valid 16-digit card number.")]
        public string CardNumber { get; set; }

        [Required]
        public string CardHolderName { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please enter a valid numeric value.")]
        [FutureMonth(ErrorMessage = "Expiration date must be the current month or a future month.")]
        public string ExpirationMonth { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please enter a valid numeric value.")]
        [FutureYear(ErrorMessage = "Expiration date must be the current year or a future year.")]
        public string ExpirationYear { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [Range(100, 999, ErrorMessage = "Invalid CVV")]
        public string CVV { get; set; }

#pragma warning restore CS8618

        public Card ToCard(int ID)
        {
            return new Card
            {
                UserId = ID,
                CardHolderName = this.CardHolderName,
                CardNumber = this.CardNumber,
                ExpirationMonth = this.ExpirationMonth,
                ExpirationYear = this.ExpirationYear,
                CVV = this.CVV
            };
        }
    }

    public class FutureMonthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(int.TryParse((string)value, out int month) == false)
            {
                return new ValidationResult(ErrorMessage);
            }
            var yearProperty = validationContext.ObjectType.GetProperty("ExpirationYear");
            var yearValue = yearProperty.GetValue(validationContext.ObjectInstance);

            if (yearValue == null || !int.TryParse(yearValue.ToString(), out var year))
            {
                return ValidationResult.Success; // Skip validation if year is not provided or invalid
            }

            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            if (year == currentYear && month < currentMonth)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

    public class FutureYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (int.TryParse((string)value, out int year) == false)
            {
                return new ValidationResult(ErrorMessage);
            }
            var currentYear = DateTime.Now.Year;

            if (year < currentYear)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
