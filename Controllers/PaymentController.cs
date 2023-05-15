using KursovaWork.Entity.Entities.Car;
using KursovaWork.Entity.Entities;
using KursovaWork.Entity;
using Microsoft.AspNetCore.Mvc;
using KursovaWork.Services;
using Microsoft.EntityFrameworkCore;

namespace KursovaWork.Controllers
{
    public class PaymentController : Controller
    {
        private readonly CarSaleContext _context;

        private readonly ILogger<PaymentController> _logger;

        private readonly IDRetriever _IDRetriever;

        private static CarInfo _curCar;

        public PaymentController(CarSaleContext context, ILogger<PaymentController> logger, IDRetriever idRetriever)
        {
            _context = context;
            _logger = logger;
            _IDRetriever = idRetriever;
        }
        public IActionResult Payment(string param1, string param2, string param3)
        {
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;

            int loggedInUserId = _IDRetriever.GetLoggedInUserId();

            if (loggedInUserId == 0)
            {
                return View("~/Views/Payment/NotLoggedIn.cshtml");
            }

            var creditCard = _context.Cards.FirstOrDefault(c => c.UserId == loggedInUserId);

            if (creditCard == null)
            {
                return View("~/Views/Payment/CardNotConnected.cshtml");
            }

            List<CarInfo> cars = _context.Cars
            .Include(o => o.Detail)
            .Include(o => o.Images)
            .ToList();

            int year = int.Parse(param3);

            foreach (var car in cars)
            {
                if (car.Make.Equals(param1) && car.Model.Equals(param2) && car.Year == year)
                {
                    _curCar = car;
                    return View(car);
                }
            }

            return View("Error");

        }
        public IActionResult Success()
        {
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;

            int loggedInUserId = _IDRetriever.GetLoggedInUserId();

            if (loggedInUserId == 0)
            {
                return View("~/Views/Payment/NotLoggedIn.cshtml");
            }

            ConfiguratorOptions temp = null;

            if (ConfiguratorController._options != null)
            {
                temp = ConfiguratorController._options;
            }

            Order order = new Order()
            {
                CarId = _curCar.Id,
                UserId = loggedInUserId,
                Price = _curCar.Price,
                OrderDate = DateTime.Now,
                ConfiguratorOptions = temp
            };

            _context.Orders.Add(order);

            _context.SaveChanges();



            return View("~/Views/Payment/Success.cshtml");
        }
    }
}
