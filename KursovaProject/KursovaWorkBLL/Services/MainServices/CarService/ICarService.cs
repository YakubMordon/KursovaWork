using KursovaWorkDAL.Entity.Entities.Car;

namespace KursovaWorkBLL.Services.MainServices.CarService
{
    /// <summary>
    /// Інтерфейс для бізнес-логіки зв'язаної з автомобілями
    /// </summary>
    public interface ICarService
    {
        /// <summary>
        /// Метод для получення інформації про автомобіль за його ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор автомобіля</param>
        /// <returns>Інформація про автомобіль</returns>
        CarInfo GetCarById(int id);

        /// <summary>
        /// Метод для получення інформації про автомобіль за його маркою, моделлю та роком виробництва
        /// </summary>
        /// <param name="make">Марка автомобіля</param>
        /// <param name="model">Модель автомобіля</param>
        /// <param name="year">Рік виробництва автомобіля</param>
        /// <returns>Інформація про автомобіль</returns>
        CarInfo GetCarByInfo(string make, string model, int year);

        /// <summary>
        /// Метод для добавлення інформації про автомобіль у бд
        /// </summary>
        /// <param name="car">Інформація про автомобіль</param>
        void AddCar(CarInfo car);

        /// <summary>
        /// Метод для обновлення інформації про автомобіль в бд
        /// </summary>
        /// <param name="car">Інформація про автомобіль</param>
        void UpdateCar(CarInfo car);

        /// <summary>
        /// Метод для видалення інформації про автомобіль з бд
        /// </summary>
        /// <param name="car">Інформація про автомобіль</param>
        void DeleteCar(CarInfo car);

        /// <summary>
        /// Метод для получення всієї можливої інформації про автомобіль
        /// </summary>
        /// <returns>Список всієї можливої інформації про автомобіль</returns>
        IEnumerable<CarInfo> GetAllCars();

        /// <summary>
        /// Метод сортування списку моделей за алфавітом.
        /// </summary>
        /// <param name="_curList">Список автомобілів</param>
        /// <returns>Відсортований список</returns>
        IEnumerable<CarInfo> SortByAlphabet(IEnumerable<CarInfo> _curList);

        /// <summary>
        /// Метод сортування списку моделей за ціною.
        /// </summary>
        /// <param name="_curList">Список автомобілів</param>
        /// <param name="param">Параметр сортування (cheap або expensive).</param>
        /// <returns>Відсортований список</returns>
        IEnumerable<CarInfo> SortByPrice(IEnumerable<CarInfo> _curList, string param);

        /// <summary>
        /// Метод сортування списку моделей за новизною (роком виробництва).
        /// </summary>
        /// <param name="_curList">Список автомобілів</param>
        /// <returns>Відсортований список</returns>
        IEnumerable<CarInfo> SortByNovelty(IEnumerable<CarInfo> _curList);

        /// <summary>
        /// Метод фільтрування списку моделей
        /// </summary>
        /// <param name="PriceFrom">Нижня межа ціни.</param>
        /// <param name="PriceTo">Верхня межа ціни.</param>
        /// <param name="YearFrom">Нижня межа року виробництва.</param>
        /// <param name="YearTo">Верхня межа року виробництва.</param>
        /// <param name="SelectedFuelTypes">Список обраних типів палива.</param>
        /// <param name="SelectedTransmissionTypes">Список обраних типів трансмісії.</param>
        /// <param name="SelectedMakes">Список обраних марок автомобілів.</param>
        /// <returns>Відфільтрований список моделей</returns>
        IEnumerable<CarInfo> Filtering(int? PriceFrom, int? PriceTo, int? YearFrom, int? YearTo, string? SelectedFuelTypes, string? SelectedTransmissionTypes, string? SelectedMakes);
    }
}
