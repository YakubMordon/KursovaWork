using KursovaWorkDAL.Entity.Entities.Car;

namespace KursovaWork.Models
{
    /// <summary>
    /// Модель представлення для фільтрації автомобілів.
    /// </summary>
    public class FilterViewModel
    {
        /// <summary>
        /// Нижня межа ціни.
        /// </summary>
        public int? PriceFrom { get; set; }

        /// <summary>
        /// Верхня межа ціни.
        /// </summary>
        public int? PriceTo { get; set; }

        /// <summary>
        /// Нижня межа року виробництва.
        /// </summary>
        public int? YearFrom { get; set; }

        /// <summary>
        /// Верхня межа року виробництва.
        /// </summary>
        public int? YearTo { get; set; }

        /// <summary>
        /// Список обраних типів палива.
        /// </summary>
        public string? SelectedFuelTypes { get; set; }

        /// <summary>
        /// Список обраних типів трансмісії.
        /// </summary>
        public string? SelectedTransmissionTypes { get; set; }

        /// <summary>
        /// Список обраних марок автомобілів.
        /// </summary>
        public string? SelectedMakes { get; set; }

        /// <summary>
        /// Список моделів, на даний момент
        /// </summary>
        public List<CarInfo> cars { get; set; }

        /// <summary>
        /// Початковий список моделів
        /// </summary>
        public static List<CarInfo> origCars {  get; set; }
    }
}
