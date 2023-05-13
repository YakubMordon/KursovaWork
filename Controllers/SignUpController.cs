
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using KursovaWork.Models;
using KursovaWork.Entity;

namespace KursovaWork.Controllers
{
    public class SignUpController : Controller
    {
        private readonly CarSaleContext _context;

        private readonly ILogger<SignUpController> _logger;

        public SignUpController(CarSaleContext context, ILogger<SignUpController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult SignUp()
        {
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;
            return View();
        }

        public IActionResult LogIn()
        {
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;
            return View("~/Views/LogIn/LogIn.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel user)
        {
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;
            if (ModelState.IsValid)
            {
                bool userExists = await _context.Users.AnyAsync(u => u.Email == user.Email);

                if (userExists)
                {
                    ModelState.AddModelError("Email", "User with this email already exists.");
                    return View(user);
                }

                _context.Add(user.ToUser());
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

    }
}
