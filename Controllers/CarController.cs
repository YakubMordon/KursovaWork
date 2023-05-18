using KursovaWorkDAL.Entity.Entities.Car;
using Microsoft.AspNetCore.Mvc;
using KursovaWorkBLL.Services.MainServices.CarService;

namespace KursovaWork.Controllers
{
    /// <summary>
    /// Контролер, що відповідає за обробку дій пов'язаних з автомобілями.
    /// </summary>
    public class CarController : Controller
    {
        /// <summary>
        /// Сервіс для роботи з автомобілями
        /// </summary>
        private readonly ICarService _carService;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<CarController> _logger;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="CarController"/>.
        /// </summary>
        /// <param name="carService">Сервіс для роботи з автомобілями.</param>
        /// <param name="logger">Логгер для запису логів.</param>
        public CarController(ICarService carService, ILogger<CarController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        /// <summary>
        /// Отримує сторінку з детальною інформацією про автомобіль.
        /// </summary>
        /// <param name="param1">Параметр 1 (марка автомобіля).</param>
        /// <param name="param2">Параметр 2 (модель автомобіля).</param>
        /// <param name="param3">Параметр 3 (рік виробництва автомобіля).</param>
        /// <returns>Страниця з детальною інформацією про автомобіль або сторінка помилки.</returns>
        public IActionResult Car(string param1, string param2, string param3)
        {
            _logger.LogInformation("Вхід у метод переходу на сторінку машини");

            int year = int.Parse(param3);

            CarInfo car = _carService.GetCarByInfo(param1, param2, year);

            if(car != null)
            {
                _logger.LogInformation("Машину знайдено, перехід на сторінку машини");
                return View(car);
            }

            _logger.LogError("Машину не знайдено");
            return View("Error");
        }

    }
}
