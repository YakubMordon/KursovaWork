using KursovaWorkDAL.Entity.Entities;
using KursovaWork.Models;
using KursovaWorkBLL.Services.MainServices.CardService;
using Microsoft.AspNetCore.Mvc;

namespace KursovaWork.Controllers
{
    /// <summary>
    /// Контролер, що відповідає за операції пов'язані з методами оплати.
    /// </summary>
    public class CreditCardController : Controller
    {
        /// <summary>
        /// Інтерфейс сервісу для опрацювання запиту
        /// </summary>
        private readonly ICardService _cardService;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<CreditCardController> _logger;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="CreditCardController"/>.
        /// </summary>
        /// <param name="cardService">Інтерфейс сервісу для опрацювання запиту</param>
        /// <param name="logger">Об'єкт класу ILogger для логування подій </param>
        public CreditCardController(ICardService cardService, ILogger<CreditCardController> logger)
        {
            _cardService = cardService;
            _logger = logger;
        }

        /// <summary>
        /// Перехід на сторінку методів оплати.
        /// </summary>
        /// <returns>Сторінка методів оплати.</returns>
        public IActionResult CreditCard()
        {
            _logger.LogInformation("Перехід до методу переходу на сторінку методів оплати");

            _logger.LogInformation("Поля для додавання карти є виключені під час прогрузки");
            ViewBag.Input = false;

            if (_cardService.CardExists())
            {
                _logger.LogInformation("Метод оплати є підключеним у користувача");

                _logger.LogInformation("Заполучення всіх даних про метод оплати користувача");

                Card user = _cardService.GetByLoggedInUser();
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

            if (ModelState.IsValid)
            {
                _cardService.AddCard(model.ToCard());
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

            _cardService.DeleteCard();

            _logger.LogInformation("Поля для додавання карти є виключені під час прогрузки");
            ViewBag.Input = false;

            _logger.LogInformation("Перехід на сторінку методів оплати");
            return View("~/Views/CreditCard/CreditCard.cshtml");
        }
    }
}
