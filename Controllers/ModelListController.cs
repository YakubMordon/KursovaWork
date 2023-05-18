using KursovaWorkDAL.Entity.Entities.Car;
using KursovaWork.Models;
using KursovaWorkBLL.Services.MainServices.CarService;
using Microsoft.AspNetCore.Mvc;

namespace KursovaWork.Controllers
{
    /// <summary>
    /// Контролер, що відповідає за відображення списку моделей автомобілів.
    /// </summary>
    public class ModelListController : Controller
    {
        /// <summary>
        /// Сервіс для виконання дій зв'язаних з автомобілями
        /// </summary>
        private readonly ICarService _carService;

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
        /// <param name="carService">Сервіс для виконання дій зв'язаних з автомобілями</param>
        /// <param name="logger">Логгер для запису логів.</param>
        public ModelListController(ICarService carService, ILogger<ModelListController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        /// <summary>
        /// Отримує сторінку списку моделей автомобілів.
        /// </summary>
        /// <returns>Сторінка списку моделей автомобілів.</returns>
        public IActionResult ModelList()
        {
            _logger.LogInformation("Вхід у метод переходу на сторінку списку моделів");
            _curList = _carService.GetAllCars().ToList();

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
            _curList = _carService.SortByAlphabet(_curList).ToList();

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
            _curList = _carService.SortByPrice(_curList, param1).ToList();

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

            _curList = _carService.SortByNovelty(_curList).ToList();

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
            _curList = _carService.Filtering(filter.PriceFrom,filter.PriceTo,
                filter.YearFrom,filter.YearTo,
                filter.SelectedFuelTypes,
                filter.SelectedTransmissionTypes,
                filter.SelectedMakes)
                .ToList();

            _logger.LogInformation("Встановлення відфільтрованого списку як поточного");

            filter.cars = _curList;

            _logger.LogInformation("Перехід на сторінку списку моделів");

            return View("~/Views/ModelList/ModelList.cshtml", filter);
        }

    }
}
