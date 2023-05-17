using KursovaWork.Entity.Entities.Car;
using KursovaWork.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KursovaWork.Entity.Entities;
using KursovaWork.Services;

namespace KursovaWork.Controllers
{
    /// <summary>
    /// Контролер, що відповідає за обробку дій пов'язаних з конфігурацією автомобіля.
    /// </summary>
    public class ConfiguratorController : Controller
    {
        /// <summary>
        /// Контекст бази даних, завдяки якому можна працювати з бд
        /// </summary>
        private readonly CarSaleContext _context;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<ConfiguratorController> _logger;

        /// <summary>
        /// Об'єкт класу CarInfo? (nullable), який вказує на поточну машину
        /// </summary>
        private static CarInfo _curCar;

        /// <summary>
        /// Об'єкт класу ConfiguratorOptions? (nullable), який вказує на обрані опції в конфігураторі
        /// </summary>
        public static ConfiguratorOptions? _options { get; set; }

        /// <summary>
        /// Масив рядків, в якому знаходяться: Марка машини - індекс 0, Модель машини - індекс 1, Рік виробництва - індекс 2
        /// </summary>
        private static string[] param = new string[3];

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="ConfiguratorController"/>.
        /// </summary>
        /// <param name="context">Контекст бази даних CarSale.</param>
        /// <param name="logger">Логгер для запису логів.</param>
        public ConfiguratorController(CarSaleContext context, ILogger<ConfiguratorController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Отримує сторінку з конфігуратором автомобіля.
        /// </summary>
        /// <param name="param1">Параметр 1 (марка автомобіля).</param>
        /// <param name="param2">Параметр 2 (модель автомобіля).</param>
        /// <param name="param3">Параметр 3 (рік виробництва автомобіля).</param>
        /// <returns>Сторінка з конфігуратором автомобіля або сторінка помилки.</returns>
        public IActionResult Configurator(string param1, string param2, string param3)
        {
            _logger.LogInformation("Вхід у функцію переходу на конфігуратор");
            List<CarInfo> cars = _context.Cars
                .Include(o => o.Detail)
                .Include(o => o.Images)
                .ToList();

            _logger.LogInformation("Зчитування усіх можливих машин з бд");

            param[0] = param1;
            param[1] = param2;
            param[2] = param3;

            int year = int.Parse(param3);

            _logger.LogInformation("Перехід у цикл ітерування по масиві");
            foreach (var car in cars)
            {
                if (car.Make.Equals(param1) && car.Model.Equals(param2) && car.Year == year)
                {
                    _curCar = car;
                    _logger.LogInformation("Машину знайдено, перехід на сторінку конфігуратора");
                    return View(car);
                }
            }

            _logger.LogError("Машину не знайдено");

            return View("Error");
        }

        /// <summary>
        /// Обробляє вибрані параметри конфігурації автомобіля.
        /// </summary>
        /// <param name="color">Колір автомобіля.</param>
        /// <param name="transmission">Тип коробки передач автомобіля.</param>
        /// <param name="fuelType">Тип палива автомобіля.</param>
        /// <returns>Сторінка оплати автомобіля або сторінка конфігуратора з повідомленнями про помилки.</returns>
        public IActionResult Submit(string color, string transmission, string fuelType)
        {
            _logger.LogInformation("Вхід у функцію підтвердження конфігурації автомобіля");

            if (string.IsNullOrEmpty(color))
            {
                ViewData["ColorError"] = "Виберіть колір";
                _logger.LogInformation("Колір не було обрано");
            }

            if (string.IsNullOrEmpty(transmission))
            {
                ViewData["TransmissionError"] = "Виберіть тип коробки передач";
                _logger.LogInformation("Тип коробки передач не було обрано");
            }

            if (string.IsNullOrEmpty(fuelType))
            {
                ViewData["FuelTypeError"] = "Виберіть тип палива";
                _logger.LogInformation("Тип палива не було обрано");
            }

            _options = new ConfiguratorOptions();

            _options.Color = color;
            _options.FuelType = fuelType;
            _options.Transmission = transmission;

            if(string.IsNullOrEmpty(color) || string.IsNullOrEmpty(transmission) || string.IsNullOrEmpty(fuelType))
            {
                _logger.LogInformation("Один або більше параметрів не було обрано в конфігураторі");
                return View("~/Views/Configurator/Configurator.cshtml", _curCar);
            }

            _logger.LogInformation("Перехід на сторінку оплати автомобіля");
            return RedirectToAction("Payment", "Payment", new
            {
                param1 = param[0],
                param2 = param[1],
                param3 = param[2]
            });
        }
    }
}
