using KursovaWork.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using KursovaWork.Services;
using Microsoft.EntityFrameworkCore;
using KursovaWork.Entity.Entities.Car;
using KursovaWork.Entity.Entities;
using KursovaWork.Entity;

namespace KursovaWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly CarSaleContext _context;

        private readonly IDRetriever _IDRetriever;

        public HomeController(CarSaleContext context, ILogger<HomeController> logger, IDRetriever idRetriever)
        {
            _context = context;
            _logger = logger;
            _IDRetriever = idRetriever;
        }

        public IActionResult Index()
        {
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;

            return View();
        }


        public IActionResult LogIn()
        {
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;

            return View("~/Views/LogIn/LogIn.cshtml");
        }

        public IActionResult OrderList()
        {
            int loggedInUserId = _IDRetriever.GetLoggedInUserId();
            List<Order> orders = _context.Orders
                .Include(o => o.Car)
                    .ThenInclude(c => c.Detail) 
                .Where(o => o.UserId == loggedInUserId)
                .ToList();
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;
            return View("~/Views/OrderList/OrderList.cshtml",orders);
        }

        public async Task<IActionResult> CreditCard()
        {
            ViewBag.Input = false;
            int loggedInUserId = _IDRetriever.GetLoggedInUserId();
            bool cardExists = await _context.Cards.AnyAsync(u => u.UserId == loggedInUserId);
            if (cardExists)
            {
                Card user = _context.Cards.FirstOrDefault(u => u.UserId == loggedInUserId);
                string cardNumber = user.CardNumber;
                ViewBag.CardNumber = "···· ···· ···· " + cardNumber.Substring(cardNumber.Length - 4);
                ViewBag.CardHolderName = user.CardHolderName;
                ViewBag.Month = user.ExpirationMonth;
                ViewBag.Year = user.ExpirationYear;
                ViewBag.Card = true;
            }
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;
            return View("~/Views/CreditCard/CreditCard.cshtml");
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}