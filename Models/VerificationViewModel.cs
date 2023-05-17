namespace KursovaWork.Models
{
    /// <summary>
    /// Клас для получення введеного верифікаційного коду
    /// </summary>
    public class VerificationViewModel
    {
        /// <summary>
        /// Масив цифри верифікаційного коду
        /// </summary>
        public string[] VerificationDigits { get; set; } = new string[4];
    }
}
