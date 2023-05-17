using KursovaWork.Entity;
using KursovaWork.Entity.Entities;
using KursovaWork.Models;
using KursovaWork.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Text;

namespace KursovaWork.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly CarSaleContext _context;

        private readonly ILogger<ChangePasswordController> _logger;

        private static User? _curUser;

        private static int _verificationCode;

        public ChangePasswordController(CarSaleContext context, ILogger<ChangePasswordController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult UserFinder()
        {
            return View("~/Views/ForgotPassword/UserFinder.cshtml");
        }
        
        public IActionResult ForgotPassword(EmailViewModel model)
        {
            if(ModelState.IsValid)
            {
                _curUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
                if (_curUser != null)
                {
                    return SendVerificationCode();
                }
                ModelState.AddModelError("", "Така електронна пошта не є зареєстрованою");
            }

            return View("~/Views/ForgotPassword/UserFinder.cshtml", model);
        }

        public IActionResult ChangePassword(VerificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var digit in model.VerificationDigits)
                {
                    if (string.IsNullOrEmpty(digit))
                    {
                        ModelState.AddModelError("VerificationDigits", "Не введено всіх цифр");
                        return View("~/Views/ForgotPassword/ForgotPassword.cshtml", model);
                    }
                    stringBuilder.Append(digit);
                }

                string temp = stringBuilder.ToString();

                if (int.Parse(temp) != _verificationCode)
                {
                    ModelState.AddModelError("VerificationDigits", "Неправильний код підтвердження");
                    return View("~/Views/ForgotPassword/ForgotPassword.cshtml", model);
                }

                return View("~/Views/ForgotPassword/ChangePassword.cshtml");
            }
            return View("~/Views/ForgotPassword/ForgotPassword.cshtml", model);
        }

        [HttpPost]
        public IActionResult SubmitChange(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == _curUser.Email);

                user.Password = model.Password;
                user.ConfirmPassword = model.ConfirmPassword;

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View("~/Views/ForgotPassword/ChangePassword.cshtml", model);
        }

        public IActionResult SendVerificationCode()
        {
            _verificationCode = new Random().Next(1000, 9999);

            string subject = "Код підтвердження";

            string body = EmailBodyTemplate.bodyTemp(_curUser.FirstName, _curUser.LastName, _verificationCode, "зміни паролю");

            EmailSender.SendEmail(_curUser.Email, subject , body);

            return View("~/Views/ForgotPassword/ForgotPassword.cshtml");
        }
    }
}
