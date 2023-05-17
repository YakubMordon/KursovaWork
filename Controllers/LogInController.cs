using KursovaWork.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using KursovaWork.Entity;

namespace KursovaWork.Controllers
{
    public class LogInController : Controller
    {
        private readonly CarSaleContext _context;

        private readonly ILogger<LogInController> _logger;

        public LogInController(CarSaleContext context, ILogger<LogInController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View("~/Views/SignUp/SignUp.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = model.ValidateUser(_context);

                if (user != null)
                {

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties).Wait();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
                
            }

            return View(model);
        }

    }


}
