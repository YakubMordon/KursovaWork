namespace KursovaWork.Models
{
    /// <summary>
    /// Представляє модель для відображення помилки.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Ідентифікатор запиту.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Визначає, чи показувати ідентифікатор запиту.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}