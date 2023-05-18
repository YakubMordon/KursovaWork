using KursovaWorkDAL.Entity.Entities;
using KursovaWork.Models;
using KursovaWorkBLL.Services.AdditionalServices;
using KursovaWorkBLL.Services.MainServices.UserService;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace KursovaWork.Controllers
{
    /// <summary>
    /// Контролер, що відповідає за зміну пароля користувача.
    /// </summary>
    public class ChangePasswordController : Controller
    {
        /// <summary>
        /// Сервіс для роботи з користувачем
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<ChangePasswordController> _logger;

        /// <summary>
        /// Об'єкт класу User? (nullable), який вказує на поточного користувача
        /// </summary>
        private static User? _curUser;

        /// <summary>
        /// Змінна, яка містить в собі поточний верифікаційний код
        /// </summary>
        private static int _verificationCode;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="ChangePasswordController"/>.
        /// </summary>
        /// <param name="userService">Сервіс для роботи з користувачем.</param>
        /// <param name="logger">Логгер для запису логів.</param>
        public ChangePasswordController(IUserService userService, ILogger<ChangePasswordController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Отримує сторінку для введення пошти користувача.
        /// </summary>
        /// <returns>Сторінка введення пошти користувача.</returns>
        public IActionResult UserFinder()
        {
            _logger.LogInformation("Перехід на сторінку введення пошти");
            return View("~/Views/ForgotPassword/UserFinder.cshtml");
        }

        /// <summary>
        /// Обробляє введену пошту користувача для відправки верифікаційного коду.
        /// </summary>
        /// <param name="model">Модель, що містить введену пошту користувача.</param>
        /// <returns>Сторінка введення верифікаційного коду або сторінка введення пошти з повідомленням про помилку.</returns>
        public IActionResult ForgotPassword(EmailViewModel model)
        {
            _logger.LogInformation("Вхід у метод верифікації електронної пошти");
            if(ModelState.IsValid)
            {
                _curUser = _userService.GetUserByEmail(model.Email);    
                if (_curUser != null)
                {
                    _logger.LogInformation("Пошту знайдено");
                    return SendVerificationCode();
                }
                ModelState.AddModelError("", "Така електронна пошта не є зареєстрованою");
                _logger.LogInformation("Електронна пошта не є зареєстрованою");
            }

            _logger.LogInformation("Дані не пройшли валідацію");
            return View("~/Views/ForgotPassword/UserFinder.cshtml", model);
        }

        /// <summary>
        /// Обробляє введений верифікаційний код для переходу до сторінки зміни пароля користувача.
        /// </summary>
        /// <param name="model">Модель, що містить введений користувачем верифікаційний код.</param>
        /// <returns>Сторінка зміни пароля або сторінка введення верифікаційного коду з повідомленням про помилку.</returns>
        public IActionResult ChangePassword(VerificationViewModel model)
        {
            _logger.LogInformation("Вхід у метод верифікації коду");
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Дані пройшли валідацію");
                StringBuilder stringBuilder = new StringBuilder();

                _logger.LogInformation("Переходимо в цикл утворення цілісного рядка з 4 підрядків");
                foreach (var digit in model.VerificationDigits)
                {
                    if (string.IsNullOrEmpty(digit))
                    {
                        ModelState.AddModelError("VerificationDigits", "Не введено всіх цифр");
                        _logger.LogInformation("Не введено всіх цифр");
                        return View("~/Views/ForgotPassword/ForgotPassword.cshtml", model);
                    }
                    stringBuilder.Append(digit);
                }

                string temp = stringBuilder.ToString();

                if (int.Parse(temp) != _verificationCode)
                {
                    ModelState.AddModelError("VerificationDigits", "Неправильний код підтвердження");
                    _logger.LogInformation("Неправильний код підтвердженння");
                    return View("~/Views/ForgotPassword/ForgotPassword.cshtml", model);
                }

                _logger.LogInformation("Переходимо на сторінку введення нового паролю");
                return View("~/Views/ForgotPassword/ChangePassword.cshtml");
            }
            _logger.LogInformation("Дані не пройшли валідацію");

            return View("~/Views/ForgotPassword/ForgotPassword.cshtml", model);
        }

        /// <summary>
        /// Обробляє введений новий пароль користувача.
        /// </summary>
        /// <param name="model">Модель, що містить введений користувачем новий пароль.</param>
        /// <returns>Сторінка зміни пароля або сторінка введення нового пароля з повідомленням про помилку.</returns>
        [HttpPost]
        public IActionResult SubmitChange(ChangePasswordViewModel model)
        {
            _logger.LogInformation("Вхід у метод зміни паролю");
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Дані пройшли валідацію");

                _userService.UpdatePasswordOfUser(model.Password, model.ConfirmPassword, _curUser);

                _logger.LogInformation("Успішно змінено пароль, переходимо на головну сторінку");

                return RedirectToAction("Index", "Home");

            }

            _logger.LogInformation("Дані не пройшли валідацію");

            return View("~/Views/ForgotPassword/ChangePassword.cshtml", model);
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

            string body = EmailBodyTemplate.BodyTemp(_curUser.FirstName, _curUser.LastName, _verificationCode, "зміни паролю");

            _logger.LogInformation("Надсилаємо повідомлення на електронну пошту користувача та переходимо на сторінку з введенням коду");

            EmailSender.SendEmail(_curUser.Email, subject , body);

            return View("~/Views/ForgotPassword/ForgotPassword.cshtml");
        }
    }
}
