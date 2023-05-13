using KursovaWork.Entity;
using KursovaWork.Entity.Entities;
using KursovaWork.Models;
using KursovaWork.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KursovaWork.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly CarSaleContext _context;

        private readonly ILogger<CreditCardController> _logger;

        private readonly IDRetriever _IDRetriever;

        public CreditCardController(CarSaleContext context, ILogger<CreditCardController> logger, IDRetriever idRetriever)
        {
            _context = context;
            _logger = logger;
            _IDRetriever = idRetriever;
        }

        [HttpGet]
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreditCard(CreditCardViewModel model)
        {
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;

            int loggedInUserId = _IDRetriever.GetLoggedInUserId();

            if (loggedInUserId == 0)
            {
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
                ViewBag.Input = false;
                return RedirectToAction("Index", "Home");

            }
            ViewBag.Input = true;
            return View(model);
        }

        public IActionResult DeleteCreditCard()
        {
            int loggedInUserId = _IDRetriever.GetLoggedInUserId();

            var creditCard = _context.Cards.FirstOrDefault(c => c.UserId == loggedInUserId);

            if (creditCard != null)
            {
                _context.Cards.Remove(creditCard);
                _context.SaveChanges();
            }

            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;
            ViewBag.Input = false;

            return View("~/Views/CreditCard/CreditCard.cshtml");
        }
    }
}
