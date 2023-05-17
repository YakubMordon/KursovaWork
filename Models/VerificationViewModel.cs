using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Models
{
    public class VerificationViewModel
    {
        public string[] VerificationDigits { get; set; } = new string[4];
    }
}
