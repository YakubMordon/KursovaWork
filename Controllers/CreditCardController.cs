using KursovaWork.Entity;
using KursovaWork.Entity.Entities;
using KursovaWork.Entity.Entities.Car;
using KursovaWork.Models;
using KursovaWork.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KursovaWork.Controllers
{
    /// <summary>
    /// Контролер, що відповідає за операції пов'язані з методами оплати.
    /// </summary>
    public class CreditCardController : Controller
    {
        /// <summary>
        /// Контекст бази даних, завдяки якому можна працювати з бд
        /// </summary>
        private readonly CarSaleContext _context;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<CreditCardController> _logger;

        /// <summary>
        /// Об'єкт класу IDRetriever для получення ідентифікатора користувача
        /// </summary>
        private readonly IDRetriever _IDRetriever;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="CreditCardController"/>.
        /// </summary>
        /// <param name="context">Контекст бази даних CarSale.</param>
        /// <param name="logger">Логгер для запису логів.</param>
        /// <param name="idRetriever">Сервіс для отримання ідентифікатора користувача.</param>
        public CreditCardController(CarSaleContext context, ILogger<CreditCardController> logger, IDRetriever idRetriever)
        {
            _context = context;
            _logger = logger;
            _IDRetriever = idRetriever;
        }

        /// <summary>
        /// Перехід на сторінку методів оплати.
        /// </summary>
        /// <returns>Сторінка методів оплати.</returns>
        public async Task<IActionResult> CreditCard()
        {
            _logger.LogInformation("Перехід до методу переходу на сторінку методів оплати");

            _logger.LogInformation("Поля для додавання карти є виключені під час прогрузки");
            ViewBag.Input = false;

            _logger.LogInformation("Заполучення ідентифікатора користувача");
            int loggedInUserId = _IDRetriever.GetLoggedInUserId();

            _logger.LogInformation("Перевірка чи у користувача є підключений метод оплати");
            bool cardExists = await _context.Cards.AnyAsync(u => u.UserId == loggedInUserId);

            if (cardExists)
            {
                _logger.LogInformation("Метод оплати є підключеним у користувача");

                _logger.LogInformation("Заполучення всіх даних про метод оплати користувача");

                Card user = _context.Cards.FirstOrDefault(u => u.UserId == loggedInUserId);
                string cardNumber = user.CardNumber;
                ViewBag.CardNumber = "···· ···· ···· " + cardNumber.Substring(cardNumber.Length - 4);
                ViewBag.CardHolderName = user.CardHolderName;
                ViewBag.Month = user.ExpirationMonth;
                ViewBag.Year = user.ExpirationYear;
                ViewBag.Card = true;
            }

            _logger.LogInformation("Перехід на сторінку методів оплати");
            return View("~/Views/CreditCard/CreditCard.cshtml");
        }

        /// <summary>
        /// Обробка форми додавання методу оплати.
        /// </summary>
        /// <param name="model">Модель з даними методу оплати.</param>
        /// <returns>Перенаправлення на головну сторінку або повторний вивід форми з помилками.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreditCard(CreditCardViewModel model)
        {
            _logger.LogInformation("Вхід до методу видалення методу оплати");

            _logger.LogInformation("Заполучення ідентифікатора користувача");
            int loggedInUserId = _IDRetriever.GetLoggedInUserId();

            if (loggedInUserId == 0)
            {
                _logger.LogInformation("Користувач не ввійшов в обліковий запис");
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                var card = new Card
                {
                    UserId = loggedInUserId,
                    CardNumber = model.CardNumber,
                    CardHolderName = model.CardHolderName,
                    ExpirationMonth = model.ExpirationMonth,
                    ExpirationYear = model.ExpirationYear,
                    CVV = model.CVV
                };

                _context.Add(card);
                _context.SaveChanges();

                _logger.LogInformation("Метод оплати було успішно додано");
                return RedirectToAction("Index", "Home");

            }

            _logger.LogInformation("Дані не пройшли верифікацію");

            _logger.LogInformation("Показуємо поля введення зразу ж після прогрузки");
            ViewBag.Input = true;
            return View(model);
        }

        /// <summary>
        /// Видалення методу оплати.
        /// </summary>
        /// <returns>Перенаправлення на сторінку методів оплати.</returns>
        public IActionResult DeleteCreditCard()
        {
            _logger.LogInformation("Вхід до методу видалення методу оплати");

            _logger.LogInformation("Заполучення ідентифікатора");
            int loggedInUserId = _IDRetriever.GetLoggedInUserId();

            _logger.LogInformation("Пошук методу оплати в базі даних");
            var creditCard = _context.Cards.FirstOrDefault(c => c.UserId == loggedInUserId);

            if (creditCard != null)
            {
                _context.Cards.Remove(creditCard);
                _context.SaveChanges();
                _logger.LogInformation("Метод оплати було успішно видалено");
            }

            _logger.LogInformation("Поля для додавання карти є виключені під час прогрузки");
            ViewBag.Input = false;

            _logger.LogInformation("Перехід на сторінку методів оплати");
            return View("~/Views/CreditCard/CreditCard.cshtml");
        }
    }
}
