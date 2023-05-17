using KursovaWork.Entity;
using KursovaWork.Entity.Entities.Car;
using KursovaWork.Models;
using KursovaWork.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace KursovaWork.Controllers
{
    /// <summary>
    /// Контролер, що відповідає за відображення списку моделей автомобілів.
    /// </summary>
    public class ModelListController : Controller
    {
        /// <summary>
        /// Контекст бази даних, завдяки якому можна працювати з бд
        /// </summary>
        private readonly CarSaleContext _context;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<ModelListController> _logger;

        /// <summary>
        /// Список об'єктів класу CarInfo, який є поточним
        /// </summary>
        private static List<CarInfo> _curList;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="ModelListController"/>.
        /// </summary>
        /// <param name="context">Контекст бази даних CarSale.</param>
        /// <param name="logger">Логгер для запису логів.</param>
        public ModelListController(CarSaleContext context, ILogger<ModelListController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Отримує сторінку списку моделей автомобілів.
        /// </summary>
        /// <returns>Сторінка списку моделей автомобілів.</returns>
        public IActionResult ModelList()
        {
            _logger.LogInformation("Вхід у метод переходу на сторінку списку моделів");
            _curList = _context.Cars
            .Include(o => o.Detail)
            .Include(o => o.Images)
            .ToList();

            _logger.LogInformation("Заполучення всіх можливих моделей автомобілів");

            _logger.LogInformation("Встановлення списку всіх моделей автомобілів як поточного");
            var model = new FilterViewModel();
            FilterViewModel.origCars = _curList;
            model.cars = _curList;

            _logger.LogInformation("Перехід на сторінку списку моделів");
            return View(model);
        }

        /// <summary>
        /// Сортує список моделей за алфавітом.
        /// </summary>
        /// <returns>Сторінка списку моделей автомобілів зі відсортованим списком.</returns>
        public IActionResult SortByAlphabet()
        {
            _logger.LogInformation("Вхід у метод сортування списку моделей за алфавітом");
            _curList = _curList.OrderByDescending(o => (o.Make + o.Model)).ToList();

            _logger.LogInformation("Встановлення посортованого списку як поточного");
            var model = new FilterViewModel();
            model.cars = _curList;


            _logger.LogInformation("Перехід на сторінку списку моделів");

            return View("~/Views/ModelList/ModelList.cshtml", model);
        }

        /// <summary>
        /// Сортує список моделей за ціною.
        /// </summary>
        /// <param name="param1">Параметр сортування (cheap або expensive).</param>
        /// <returns>Сторінка списку моделей автомобілів зі відсортованим списком.</returns>
        public IActionResult SortByPrice(string param1)
        {
            _logger.LogInformation("Вхід у метод сортування списку моделей за ціною");

            if (param1.Equals("cheap"))
            {
                _curList = _curList.OrderBy(o => o.Price).ToList();
                _logger.LogInformation("Сортування за зростанням ціни");
            }
            else
            {
                _curList = _curList.OrderByDescending(o => o.Price).ToList();
                _logger.LogInformation("Сортування за спаданням ціни");
            }

            _logger.LogInformation("Встановлення посортованого списку як поточного");

            var model = new FilterViewModel();
            model.cars = _curList;

            _logger.LogInformation("Перехід на сторінку списку моделів");

            return View("~/Views/ModelList/ModelList.cshtml", model);
        }

        /// <summary>
        /// Сортує список моделей за новизною (роком виробництва).
        /// </summary>
        /// <returns>Сторінка списку моделей автомобілів зі відсортованим списком.</returns>
        public IActionResult SortByNovelty()
        {
            _logger.LogInformation("Вхід у метод сортування списку моделей за роком по спаданню (Новинки)");

            _curList = _curList.OrderByDescending(o => o.Year).ToList();

            _logger.LogInformation("Встановлення посортованого списку як поточного");

            var model = new FilterViewModel();
            model.cars = _curList;

            _logger.LogInformation("Перехід на сторінку списку моделів");

            return View("~/Views/ModelList/ModelList.cshtml", model);
        }

        /// <summary>
        /// Застосовує фільтри до списку моделей автомобілів.
        /// </summary>
        /// <param name="filter">Модель, що містить введені користувачем фільтри.</param>
        /// <returns>Сторінка списку моделей автомобілів з відфільтрованим списком.</returns>
        public IActionResult ApplyFilters(FilterViewModel filter)
        {
            _logger.LogInformation("Вхід у метод фільтрування списку автомобілів");
            var filteredCars = _context.Cars
                .Include(o => o.Detail)
                .Include(o => o.Images)
                .ToList();

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

            _logger.LogInformation("Встановлення відфільтрованого списку як поточного");
            _curList = filteredCars.ToList();

            filter.cars = _curList;

            _logger.LogInformation("Перехід на сторінку списку моделів");

            return View("~/Views/ModelList/ModelList.cshtml", filter);
        }

    }
}
