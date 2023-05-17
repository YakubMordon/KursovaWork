using KursovaWork.Entity.Entities.Car;
using KursovaWork.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KursovaWork.Services;

namespace KursovaWork.Controllers
{
    /// <summary>
    /// Контролер, що відповідає за обробку дій пов'язаних з автомобілями.
    /// </summary>
    public class CarController : Controller
    {
        /// <summary>
        /// Контекст бази даних, завдяки якому можна працювати з бд
        /// </summary>
        private readonly CarSaleContext _context;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<CarController> _logger;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="CarController"/>.
        /// </summary>
        /// <param name="context">Контекст бази даних CarSale.</param>
        /// <param name="logger">Логгер для запису логів.</param>
        public CarController(CarSaleContext context, ILogger<CarController> logger)
        {
            _context = context;
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
            List<CarInfo> cars = _context.Cars
                .Include(o => o.Detail)
                .Include(o => o.Images)
                .ToList();

            _logger.LogInformation("Зчитування усіх можливих машин з бд");

            int year = int.Parse(param3);

            _logger.LogInformation("Перехід у цикл ітерування по масиві");
            foreach (var car in cars)
            {
                _logger.LogInformation("Машина: " + car.Make + " " + car.Model + " " + car.Year + " року");
                if (car.Make.Equals(param1) && car.Model.Equals(param2) && car.Year == year)
                {
                    _logger.LogInformation("Машину знайдено, перехід на сторінку машини");
                    return View(car);
                }
            }

            _logger.LogError("Машину не знайдено");
            return View("Error");
        }

    }
}
