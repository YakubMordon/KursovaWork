using KursovaWorkDAL.Entity.Entities.Car;
using KursovaWorkDAL.Entity.Entities;
using KursovaWorkBLL.Services.AdditionalServices;
using Microsoft.AspNetCore.Mvc;
using KursovaWorkBLL.Services.MainServices.CarService;
using KursovaWorkBLL.Services.MainServices.CardService;
using KursovaWorkBLL.Services.MainServices.OrderService;
using KursovaWorkBLL.Services.MainServices.UserService;

namespace KursovaWork.Controllers
{
    /// <summary>
    /// Контролер для обробки оплати.
    /// </summary>
    public class PaymentController : Controller
    {
        /// <summary>
        /// Сервіс для виконання дій зв'язаних з автомобілями
        /// </summary>
        private readonly ICarService _carService;

        /// <summary>
        /// Сервіс для виконання дій зв'язаних з картою
        /// </summary>
        private readonly ICardService _cardService;

        /// <summary>
        /// Сервіс для виконання дій зв'язаних з замовленнями
        /// </summary>
        private readonly IOrderService _orderService;

        /// <summary>
        /// Сервіс для виконання дії зв'язаних з користувачем
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<PaymentController> _logger;

        /// <summary>
        /// Об'єкт класу CarInfo, який вказує на поточну машину
        /// </summary>
        private static CarInfo _curCar;

        /// <summary>
        /// Ініціалізує новий екземпляр класу PaymentController.
        /// </summary>
        /// <param name="carService">Сервіс для виконання дій зв'язаних з автомобілями.</param>
        /// <param name="cardService">Сервіс для виконання дій зв'язаних з картою</param>
        /// <param name="orderService">Сервіс для виконання дій зв'язаних з замовленнями</param>
        /// <param name="userService">Сервіс для виконання дії зв'язаних з користувачем</param>
        /// <param name="logger">Об'єкт логування ILogger.</param>
        public PaymentController(ICarService carService, ICardService cardService, IOrderService orderService, IUserService userService, ILogger<PaymentController> logger)
        {
            _carService = carService;
            _cardService = cardService;
            _orderService = orderService;
            _userService = userService; 
            _logger = logger;
        }

        /// <summary>
        /// Метод для обробки перевірки можливості оплати.
        /// </summary>
        /// <param name="param1">Марка автомобіля</param>
        /// <param name="param2">Модель автомобіля</param>
        /// <param name="param3">Рік виробництва автомобіля</param>
        /// <returns>Результат операції.</returns>
        public IActionResult Payment(string param1, string param2, string param3)
        {
            _logger.LogInformation("Вхід у метод перевірки можливості оплати");

            _logger.LogInformation("Заполучення ідентифікатора користувача");
            User user = _userService.GetLoggedInUser();

            if (user == null)
            {
                _logger.LogInformation("Користувач не ввійшов у обліковий запис");
                return View("~/Views/Payment/NotLoggedIn.cshtml");
            }

            _logger.LogInformation("Заполучення даних про метод оплати користувача");
            var creditCard = _cardService.GetByLoggedInUser();

            if (creditCard == null)
            {
                _logger.LogInformation("Користувач не додав методу оплати");
                return View("~/Views/Payment/CardNotConnected.cshtml");
            }
            int year = int.Parse(param3);

            CarInfo car = _carService.GetCarByInfo(param1, param2, year);

            if (car != null)
            {
                _curCar = car;
                _logger.LogInformation("Модель успішно знайдена");

                _logger.LogInformation("Перехід до підтвердження оплати");

                return View(car);
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

            int id = _orderService.AddOrderLoggedIn(_curCar, ConfiguratorController.options);

            ConfiguratorController.options = null;

            _logger.LogInformation("Номер замовлення повернено");

            _logger.LogInformation("Заполучення даних про користувача");
            User user = _userService.GetLoggedInUser();

            string userName = user.FirstName + " " + user.LastName;
            string userEmail = user.Email;

            string subject = $"Покупка автомобіля №{id}";
            string body = EmailBodyTemplate.OrderBodyTemp(userName, _curCar.Make, _curCar.Model, _curCar.Year);

            EmailSender.SendEmail(userEmail, subject, body);

            _logger.LogInformation("Надіслання на пошту даних про замовлення");

            _logger.LogInformation("Перехід на сторінку успішного виконання покупки");

            return View("~/Views/Payment/Success.cshtml");
        }
    }
}
