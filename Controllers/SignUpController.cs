using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KursovaWork.Models;
using KursovaWork.Entity;
using KursovaWork.Entity.Entities;
using KursovaWork.Services;
using System.Text;

namespace KursovaWork.Controllers
{
    public class SignUpController : Controller
    {
        private readonly CarSaleContext _context;

        private readonly ILogger<SignUpController> _logger;

        private static User? _curUser;

        private static int _verificationCode;

        public SignUpController(CarSaleContext context, ILogger<SignUpController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View("~/Views/LogIn/LogIn.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel user)
        {
            if (ModelState.IsValid)
            {
                bool userExists = await _context.Users.AnyAsync(u => u.Email == user.Email);

                if (userExists)
                {
                    ModelState.AddModelError("Email", "User with this email already exists.");
                    return View(user);
                }
                _curUser = user.ToUser();

                return SendVerificationCode();

            }
            return View(user);
        }
        

        [HttpPost]
        public async Task<IActionResult> Submit(VerificationViewModel verification)
        {
            if (ModelState.IsValid)
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach(var digit in verification.VerificationDigits)
                {
                    if (string.IsNullOrEmpty(digit))
                    {
                        ModelState.AddModelError("VerificationDigits", "Не введено всіх цифр");
                        return View("~/Views/SignUp/Submit.cshtml", verification);
                    }
                    stringBuilder.Append(digit);
                }

                string temp = stringBuilder.ToString();

                if(int.Parse(temp) != _verificationCode)
                {
                    ModelState.AddModelError("VerificationDigits", "Неправильний код підтвердження");
                    return View("~/Views/SignUp/Submit.cshtml", verification);
                }

                _context.Add(_curUser);
                await _context.SaveChangesAsync();
                _curUser = null;
                
                return RedirectToAction("Index", "Home");
            }
            return View("~/Views/SignUp/Submit.cshtml", verification);
        }

        public IActionResult SendVerificationCode()
        {

            _verificationCode = new Random().Next(1000, 9999);

            string subject = "Код підтвердження";

            string body = EmailBodyTemplate.bodyTemp(_curUser.FirstName, _curUser.LastName, _verificationCode, "реєстрації");

            EmailSender.SendEmail(_curUser.Email, subject, body);

            return View("~/Views/SignUp/Submit.cshtml");
        }
    }
}
