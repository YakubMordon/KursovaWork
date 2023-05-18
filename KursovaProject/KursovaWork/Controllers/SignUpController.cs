using Microsoft.AspNetCore.Mvc;
using KursovaWork.Models;
using KursovaWorkDAL.Entity.Entities;
using KursovaWorkBLL.Services.AdditionalServices;
using System.Text;
using KursovaWorkBLL.Services.MainServices.UserService;

namespace KursovaWork.Controllers
{
    /// <summary>
    /// Контролер, що відповідає за реєстрацію нового користувача.
    /// </summary>
    public class SignUpController : Controller
    {
        /// <summary>
        /// Сервіс для роботи з користувачем
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<SignUpController> _logger;

        /// <summary>
        /// Об'єкт класу User? (nullable), який вказує на поточного користувача
        /// </summary>
        private static User? _curUser;

        /// <summary>
        /// Змінна, яка містить в собі поточний верифікаційний код
        /// </summary>
        private static int _verificationCode;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="SignUpController"/>.
        /// </summary>
        /// <param name="userService">Сервіс для роботи з користувачем</param>
        /// <param name="logger">Логгер для запису логів.</param>
        public SignUpController(IUserService userService, ILogger<SignUpController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Отримує сторінку реєстрації.
        /// </summary>
        /// <returns>Сторінка реєстрації.</returns>
        public IActionResult SignUp()
        {
            _logger.LogInformation("Перехід на сторінку реєстрації");
            return View();
        }

        /// <summary>
        /// Отримує сторінку входу.
        /// </summary>
        /// <returns>Сторінка входу.</returns>
        public IActionResult LogIn()
        {
            _logger.LogInformation("Перехід на сторінку входу");
            return View("~/Views/LogIn/LogIn.cshtml");
        }

        /// <summary>
        /// Обробляє введені користувачем дані реєстрації.
        /// </summary>
        /// <param name="model">Модель, що містить введені користувачем дані реєстрації.</param>
        /// <returns>Сторінка введення верифікаційного коду або сторінка реєстрації з повідомленням про помилку.</returns>
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            _logger.LogInformation("Вхід у метод верифікації даних реєстрації");
            if (ModelState.IsValid)
            {
                User user = _userService.GetUserByEmail(model.Email);

                if (user != null)
                {
                    ModelState.AddModelError("Email", "User with this email already exists.");
                    _logger.LogInformation("Користувач з такою ж елекронною поштою існує");
                    return View(model);
                }
                _curUser = model.ToUser();
                _logger.LogInformation("Успішно перевірено чи є така ж електронна пошта");
                return SendVerificationCode();

            }
            _logger.LogInformation("Дані не пройшли валідацію");
            return View(model);
        }

        /// <summary>
        /// Обробляє введений користувачем верифікаційний код та здійснює підтвердження реєстрації.
        /// </summary>
        /// <param name="verification">Модель, що містить введений користувачем верифікаційний код.</param>
        /// <returns>Сторінка головного меню або сторінка введення верифікаційного коду з повідомленням про помилку.</returns>
        [HttpPost]
        public IActionResult Submit(VerificationViewModel verification)
        {
            _logger.LogInformation("Вхід у метод підтвердження реєстрації та перевірки валідаційного коду");

            if (ModelState.IsValid)
            {
                StringBuilder stringBuilder = new StringBuilder();
                _logger.LogInformation("Переходимо в цикл утворення цілісного рядка з 4 підрядків");
                foreach (var digit in verification.VerificationDigits)
                {
                    if (string.IsNullOrEmpty(digit))
                    {
                        ModelState.AddModelError("VerificationDigits", "Не введено всіх цифр");
                        _logger.LogInformation("Не введено всіх цифр");
                        return View("~/Views/SignUp/Submit.cshtml", verification);
                    }
                    stringBuilder.Append(digit);
                }

                string temp = stringBuilder.ToString();

                if(int.Parse(temp) != _verificationCode)
                {
                    ModelState.AddModelError("VerificationDigits", "Неправильний код підтвердження");
                    _logger.LogInformation("Неправильний код підтвердження");
                    return View("~/Views/SignUp/Submit.cshtml", verification);
                }

                _userService.AddUser(_curUser);
                _curUser = null;
                _logger.LogInformation("Успішно зареєстровано користувача, перехід на головну сторінку");
                return RedirectToAction("Index", "Home");
            }

            _logger.LogInformation("Дані не пройшли валідацію");
            return View("~/Views/SignUp/Submit.cshtml", verification);
        }

        /// <summary>
        /// Відправляє електронний лист з верифікаційним кодом.
        /// </summary>
        /// <returns>Сторінка введення верифікаційного коду.</returns>
        public IActionResult SendVerificationCode()
        {
            _logger.LogInformation("Вхід у метод надсилання верифікаційного коду");

            _verificationCode = new Random().Next(1000, 9999);

            string subject = "Код підтвердження";

            string body = EmailBodyTemplate.BodyTemp(_curUser.FirstName, _curUser.LastName, _verificationCode, "реєстрації");

            _logger.LogInformation("Надсилаємо повідомлення на електронну пошту користувача та переходимо на сторінку з введенням коду");

            EmailSender.SendEmail(_curUser.Email, subject, body);

            return View("~/Views/SignUp/Submit.cshtml");
        }
    }
}
