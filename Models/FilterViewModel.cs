using KursovaWork.Entity.Entities.Car;

namespace KursovaWork.Models
{
    public class FilterViewModel
    {
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
        public List<string> SelectedFuelTypes { get; set; }
        public List<string> SelectedTransmissionTypes { get; set; }
        public List<string> SelectedMakes { get; set; }

        public List<CarInfo> cars { get; set; }

        public static List<CarInfo> origCars {  get; set; }
    }
}
