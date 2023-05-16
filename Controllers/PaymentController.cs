using KursovaWork.Entity.Entities.Car;
using KursovaWork.Entity.Entities;
using KursovaWork.Entity;
using KursovaWork.Services;
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

            Order order = new Order()
            {
                CarId = _curCar.Id,
                UserId = loggedInUserId,
                Price = _curCar.Price,
                OrderDate = DateTime.Now
            };

            if (ConfiguratorController._options != null)
            {
                order.ConfiguratorOptions = ConfiguratorController._options;
                ConfiguratorController._options = null;
            }

            _context.Orders.Add(order);

            _context.SaveChanges();
            User user = _context.Users.SingleOrDefault(o => o.Id == loggedInUserId);
            string userName = user.FirstName + " " + user.LastName;
            string userEmail = user.Email;

            string subject = $"Покупка автомобіля №{order.Id}";
            string body = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            font-size: 14px;
                        }}
                    </style>
                </head>
                <body>
                    <h2>Шановний(а) {userName},</h2>
                    <p>Дякуємо за вашу покупку!</p>
                    <p>Ви придбали новий автомобіль {_curCar.Make} {_curCar.Model}, {_curCar.Year} року виробництва.</p>
                    <p>Деталі вашого замовлення:</p>
                    <ul>
                        <li>Марка: {_curCar.Make}</li>
                        <li>Модель: {_curCar.Model}</li>
                        <li>Рік виробництва: {_curCar.Year} рік</li>
                    </ul>
                    <p>Додаткова інформація про замовлення знаходиться у нас на сайті в вашому особистому кабінеті</p>
                    <p>Якщо у вас виникнуть будь-які питання або потреба у додатковій інформації, будь ласка, зв'яжіться з нашою службою підтримки.</p>
                    <p>Дякуємо за вашу довіру!</p>
                    <p>З повагою,</p>
                    <p>VAG Dealer</p>
                </body>
                </html>";

            EmailSender.SendEmail(userEmail, subject, body);

            return View("~/Views/Payment/Success.cshtml");
        }
    }
}
