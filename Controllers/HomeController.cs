using KursovaWork.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using KursovaWork.Services;
using Microsoft.EntityFrameworkCore;
using KursovaWork.Entity.Entities;
using KursovaWork.Entity;
using KursovaWork.Entity.Entities.Car;

namespace KursovaWork.Controllers
{
    /// <summary>
    /// Контролер, що відповідає за основні дії на головній сторінці.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Контекст бази даних, завдяки якому можна працювати з бд
        /// </summary>
        private readonly CarSaleContext _context;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Об'єкт класу IDRetriever для получення ідентифікатора користувача
        /// </summary>
        private readonly IDRetriever _IDRetriever;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="HomeController"/>.
        /// </summary>
        /// <param name="context">Контекст бази даних CarSale.</param>
        /// <param name="logger">Логгер для запису логів.</param>
        /// <param name="idRetriever">Сервіс для отримання ідентифікатора користувача.</param>
        public HomeController(CarSaleContext context, ILogger<HomeController> logger, IDRetriever idRetriever)
        {
            _context = context;
            _logger = logger;
            _IDRetriever = idRetriever;
        }

        /// <summary>
        /// Перехід на головну сторінку.
        /// </summary>
        /// <returns>Головна сторінка.</returns>
        public IActionResult Index()
        {
            _logger.LogInformation("Перехід на головну сторінку");
            return View();
        }

        /// <summary>
        /// Перехід на сторінку входу.
        /// </summary>
        /// <returns>Сторінка входу.</returns>
        public IActionResult LogIn()
        {
            _logger.LogInformation("Перехід на сторінку входу");
            return View("~/Views/LogIn/LogIn.cshtml");
        }

        /// <summary>
        /// Виконання виходу з облікового запису.
        /// </summary>
        /// <returns>Перенаправлення на головну сторінку.</returns>
        public IActionResult LogOut()
        {
            _logger.LogInformation("Виконування виходу з облікового запису");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            _logger.LogInformation("Перехід на головну сторінку");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Перехід до списку моделей.
        /// </summary>
        /// <returns>Перенаправлення на сторінку списку моделей автомобілів.</returns>
        public IActionResult ModelList()
        {
            _logger.LogInformation("Перехід до списку моделей");
            return RedirectToAction("ModelList", "ModelList");
        }

        /// <summary>
        /// Перехід на сторінку списку замовлень.
        /// </summary>
        /// <returns>Сторінка списку замовлень.</returns>
        public IActionResult OrderList()
        {
            _logger.LogInformation("Вхід у метод переходу на сторінку списку замовлень");

            _logger.LogInformation("Заполучення Ідентифікатора користувача");
            int loggedInUserId = _IDRetriever.GetLoggedInUserId();

            _logger.LogInformation("Заполучення всіх можливих замовлень, які закріплені за користувачем");
            var orders = _context.Orders
                .Include(o => o.Car)
                    .ThenInclude(c => c.Detail) 
                .Include(o => o.ConfiguratorOptions)
                .Where(o => o.UserId == loggedInUserId)
                .ToList();

            _logger.LogInformation("Перехід на сторінку списку замовлень");

            return View("~/Views/OrderList/OrderList.cshtml",orders);
        }
        
        /// <summary>
        /// Обробка помилки під час виконання запиту.
        /// </summary>
        /// <returns>Сторінка з відображенням помилки.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("Сталася помилка під час прогрузки сайту");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}