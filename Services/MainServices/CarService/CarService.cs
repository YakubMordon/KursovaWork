using KursovaWork.Entity.Entities.Car;
using KursovaWork.Models;
using KursovaWork.Repositories.CarRepository;
using Microsoft.EntityFrameworkCore;

namespace KursovaWork.Services.MainServices.CarService
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
        public IEnumerable<CarInfo> Filtering(FilterViewModel filter)
        {
            _logger.LogInformation("Вхід у метод фільтрування списку автомобілів");
            var filteredCars = _carRepository.GetAll();

            _logger.LogInformation("Заполучаємо список усіх можливих автомобілів");

            if (filter.PriceFrom.HasValue)
            {
                filteredCars = filteredCars.Where(c => c.Price >= filter.PriceFrom.Value).ToList();
                _logger.LogInformation("Фільтруємо за ціною від певного значення");
            }

            if (filter.PriceTo.HasValue)
            {
                filteredCars = filteredCars.Where(c => c.Price <= filter.PriceTo.Value).ToList();
                _logger.LogInformation("Фільтруємо за ціною до певного значення");
            }

            if (filter.YearFrom.HasValue)
            {
                filteredCars = filteredCars.Where(c => c.Year >= filter.YearFrom.Value).ToList();
                _logger.LogInformation("Фільтруємо за роком виробництва від певного значення");
            }

            if (filter.YearTo.HasValue)
            {
                filteredCars = filteredCars.Where(c => c.Year <= filter.YearTo.Value).ToList();
                _logger.LogInformation("Фільтруємо за роком виробництва до певного значення");
            }

            if (filter.SelectedFuelTypes != null && filter.SelectedFuelTypes.Any())
            {
                filteredCars = filteredCars.Where(c => filter.SelectedFuelTypes.Contains(c.Detail.FuelType)).ToList();
                _logger.LogInformation("Фільтруємо за обраним типом палива");
            }

            if (filter.SelectedTransmissionTypes != null && filter.SelectedTransmissionTypes.Any())
            {
                filteredCars = filteredCars.Where(c => filter.SelectedTransmissionTypes.Contains(c.Detail.Transmission)).ToList();
                _logger.LogInformation("Фільтруємо за обраним типом коробки передач");
            }

            if (filter.SelectedMakes != null && filter.SelectedMakes.Any())
            {
                filteredCars = filteredCars.Where(c => filter.SelectedMakes.Contains(c.Make)).ToList();
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
