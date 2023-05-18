using KursovaWorkDAL.Entity.Entities.Car;
using KursovaWorkDAL.Repositories.CarRepository;
using Microsoft.Extensions.Logging;

namespace KursovaWorkBLL.Services.MainServices.CarService
{
    /// <summary>
    /// Імплементація інтерфейсу ICarService для бізнес-логіки зв'язаної з автомобілями
    /// </summary>
    public class CarService : ICarService
    {
        /// <summary>
        /// Репозиторій автомобілів, завдяки якому працюємо з бд
        /// </summary>
        private readonly ICarRepository _carRepository;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<CarService> _logger;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="CarService"/>.
        /// </summary>
        /// <param name="carRepository">Репозиторій автомобілів</param>
        /// <param name="logger">Об'єкт класу ILogger для логування подій </param>
        public CarService(ICarRepository carRepository, ILogger<CarService> logger) 
        { 
            _carRepository = carRepository;
            _logger = logger;
        }
        public void AddCar(CarInfo car)
        {
            _carRepository.Add(car);
            _logger.LogInformation("Додано нову інформацію про автомобіль");
        }
        public void DeleteCar(CarInfo car)
        {
            _carRepository.Delete(car);
            _logger.LogInformation("Видалено інформацію про автомобіль");
        }
        public IEnumerable<CarInfo> Filtering(int? PriceFrom, int? PriceTo, int? YearFrom, int? YearTo, string? SelectedFuelTypes, string? SelectedTransmissionTypes, string? SelectedMakes)
        {
            _logger.LogInformation("Вхід у метод фільтрування списку автомобілів");
            var filteredCars = _carRepository.GetAll();

            _logger.LogInformation("Заполучаємо список усіх можливих автомобілів");

            if (PriceFrom.HasValue)
            {
                filteredCars = filteredCars.Where(c => c.Price >= PriceFrom.Value).ToList();
                _logger.LogInformation("Фільтруємо за ціною від певного значення");
            }

            if (PriceTo.HasValue)
            {
                filteredCars = filteredCars.Where(c => c.Price <= PriceTo.Value).ToList();
                _logger.LogInformation("Фільтруємо за ціною до певного значення");
            }

            if (YearFrom.HasValue)
            {
                filteredCars = filteredCars.Where(c => c.Year >= YearFrom.Value).ToList();
                _logger.LogInformation("Фільтруємо за роком виробництва від певного значення");
            }

            if (YearTo.HasValue)
            {
                filteredCars = filteredCars.Where(c => c.Year <= YearTo.Value).ToList();
                _logger.LogInformation("Фільтруємо за роком виробництва до певного значення");
            }

            if (SelectedFuelTypes != null )
            {
                filteredCars = filteredCars.Where(c => SelectedFuelTypes.Equals(c.Detail.FuelType)).ToList();
                _logger.LogInformation("Фільтруємо за обраним типом палива");
            }

            if (SelectedTransmissionTypes != null)
            {
                filteredCars = filteredCars.Where(c => SelectedTransmissionTypes.Equals(c.Detail.Transmission)).ToList();
                _logger.LogInformation("Фільтруємо за обраним типом коробки передач");
            }

            if (SelectedMakes != null)
            {
                filteredCars = filteredCars.Where(c => SelectedMakes.Equals(c.Make)).ToList();
                _logger.LogInformation("Фільтруємо за обраними марками");
            }

            return filteredCars;
        }
        public IEnumerable<CarInfo> GetAllCars()
        {
            _logger.LogInformation("Заполучено всю можливу інформацію про автомобілі");
            return _carRepository.GetAll();
        }
        public CarInfo GetCarById(int id)
        {
            _logger.LogInformation("Заполучено інформацію про автомобіль за його ідентифікатором");
            return _carRepository.GetById(id);
        }
        public CarInfo GetCarByInfo(string make, string model, int year)
        {
            _logger.LogInformation("Заполучено інформацію про автомобіль за його маркою, моделлю та роком виробництва");
            return _carRepository.GetByCarInfo(make, model, year);
        }
        public IEnumerable<CarInfo> SortByAlphabet(IEnumerable<CarInfo> _curList)
        {
            _logger.LogInformation("Вхід у метод сортування списку моделей за алфавітом");
            return _curList.OrderBy(o => (o.Make + o.Model));
        }
        public IEnumerable<CarInfo> SortByNovelty(IEnumerable<CarInfo> _curList)
        {
            _logger.LogInformation("Вхід у метод сортування списку моделей за новинками(роком виробництва по спаданню)");
            return _curList.OrderByDescending(o => o.Year);
        }
        public IEnumerable<CarInfo> SortByPrice(IEnumerable<CarInfo> _curList, string param)
        {
            _logger.LogInformation("Вхід у метод сортування списку моделей за ціною");

            if (param.Equals("cheap"))
            {
                _curList = _curList.OrderBy(o => o.Price);
                _logger.LogInformation("Сортування за зростанням ціни");
            }
            else
            {
                _curList = _curList.OrderByDescending(o => o.Price);
                _logger.LogInformation("Сортування за спаданням ціни");
            }

            return _curList;
        }
        public void UpdateCar(CarInfo car)
        {
            _carRepository.Update(car);
            _logger.LogInformation("Обновлено інформацію про автомобіль");
        }
    }
}
