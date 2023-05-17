using KursovaWork.Entity.Entities.Car;
using KursovaWork.Entity.Entities;
using KursovaWork.Entity;
using KursovaWork.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KursovaWork.Controllers
{
    /// <summary>
    /// Контролер для обробки оплати.
    /// </summary>
    public class PaymentController : Controller
    {
        /// <summary>
        /// Контекст бази даних, завдяки якому можна працювати з бд
        /// </summary>
        private readonly CarSaleContext _context;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<PaymentController> _logger;

        /// <summary>
        /// Об'єкт класу IDRetriever для получення ідентифікатора користувача
        /// </summary>
        private readonly IDRetriever _IDRetriever;

        /// <summary>
        /// Об'єкт класу CarInfo, який вказує на поточну машину
        /// </summary>
        private static CarInfo _curCar;

        /// <summary>
        /// Ініціалізує новий екземпляр класу PaymentController.
        /// </summary>
        /// <param name="context">Контекст бази даних CarSaleContext.</param>
        /// <param name="logger">Об'єкт логування ILogger.</param>
        /// <param name="idRetriever">Об'єкт для отримання ідентифікатора.</param>
        public PaymentController(CarSaleContext context, ILogger<PaymentController> logger, IDRetriever idRetriever)
        {
            _context = context;
            _logger = logger;
            _IDRetriever = idRetriever;
        }

        /// <summary>
        /// Метод для обробки перевірки можливості оплати.
        /// </summary>
        /// <param name="param1">Перший параметр.</param>
        /// <param name="param2">Другий параметр.</param>
        /// <param name="param3">Третій параметр.</param>
        /// <returns>Результат операції.</returns>
        public IActionResult Payment(string param1, string param2, string param3)
        {
            _logger.LogInformation("Вхід у метод перевірки можливості оплати");

            _logger.LogInformation("Заполучення ідентифікатора користувача");
            int loggedInUserId = _IDRetriever.GetLoggedInUserId();

            if (loggedInUserId == 0)
            {
                _logger.LogInformation("Користувач не ввійшов у обліковий запис");
                return View("~/Views/Payment/NotLoggedIn.cshtml");
            }

            _logger.LogInformation("Заполучення даних про метод оплати користувача");
            var creditCard = _context.Cards.FirstOrDefault(c => c.UserId == loggedInUserId);

            if (creditCard == null)
            {
                _logger.LogInformation("Користувач не додав методу оплати");
                return View("~/Views/Payment/CardNotConnected.cshtml");
            }

            List<CarInfo> cars = _context.Cars
            .Include(o => o.Detail)
            .Include(o => o.Images)
            .ToList();

            _logger.LogInformation("Заполучення всіх можливих моделей з бази даних"); 

            int year = int.Parse(param3);

            _logger.LogInformation("Перехід цикл ітерування у масиві");
            foreach (var car in cars)
            {
                if (car.Make.Equals(param1) && car.Model.Equals(param2) && car.Year == year)
                {
                    _curCar = car;
                    _logger.LogInformation("Модель успішно знайдена");

                    _logger.LogInformation("Перехід до підтвердження оплати");

                    return View(car);
                }
            }

            _logger.LogWarning("Моделі не було знайдено");
            return View("Error");

        }

        /// <summary>
        /// Метод для обробки успішного платежу.
        /// </summary>
        /// <returns>Результат операції.</returns>
        public IActionResult Success()
        {
            _logger.LogInformation("Перехід до методу підтвердження оплати за покупку");

            _logger.LogInformation("Заполучення ідентифікатора користувача");
            int loggedInUserId = _IDRetriever.GetLoggedInUserId();

            Order order = new Order()
            {
                CarId = _curCar.Id,
                UserId = loggedInUserId,
                Price = _curCar.Price,
                OrderDate = DateTime.Now
            };

            _logger.LogInformation("Створення заказу автомобіля");

            if (ConfiguratorController._options != null)
            {
                order.ConfiguratorOptions = ConfiguratorController._options;
                ConfiguratorController._options = null;
                _logger.LogInformation("Автомобіль було обрано у конфігураторі");
            }

            _context.Orders.Add(order);

            _context.SaveChanges();
            _logger.LogInformation("Заказ є успішно доданий");

            _logger.LogInformation("Заполучення даних про користувача");
            User user = _context.Users.SingleOrDefault(o => o.Id == loggedInUserId);
            string userName = user.FirstName + " " + user.LastName;
            string userEmail = user.Email;

            string subject = $"Покупка автомобіля №{order.Id}";
            string body = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            font-size: 14px;
                        }}
                    </style>
                </head>
                <body>
                    <h2>Шановний(а) {userName},</h2>
                    <p>Дякуємо за вашу покупку!</p>
                    <p>Ви придбали новий автомобіль {_curCar.Make} {_curCar.Model}, {_curCar.Year} року виробництва.</p>
                    <p>Деталі вашого замовлення:</p>
                    <ul>
                        <li>Марка: {_curCar.Make}</li>
                        <li>Модель: {_curCar.Model}</li>
                        <li>Рік виробництва: {_curCar.Year} рік</li>
                    </ul>
                    <p>Додаткова інформація про замовлення знаходиться у нас на сайті в вашому особистому кабінеті</p>
                    <p>Якщо у вас виникнуть будь-які питання або потреба у додатковій інформації, будь ласка, зв'яжіться з нашою службою підтримки.</p>
                    <p>Дякуємо за вашу довіру!</p>
                    <p>З повагою,</p>
                    <p>VAG Dealer</p>
                </body>
                </html>";

            EmailSender.SendEmail(userEmail, subject, body);

            _logger.LogInformation("Надіслання на пошту даних про замовлення");

            _logger.LogInformation("Перехід на сторінку успішного виконання покупки");

            return View("~/Views/Payment/Success.cshtml");
        }
    }
}
