using KursovaWork.Entity.Entities.Car;
using KursovaWork.Entity;
using KursovaWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KursovaWork.Controllers
{
    public class CarController : Controller
    {
        private readonly CarSaleContext _context;

        private readonly ILogger<CarController> _logger;

        public CarController(CarSaleContext context, ILogger<CarController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Car(string param1, string param2, string param3)
        {
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;

            List<CarInfo> cars = _context.Cars
                .Include(o => o.Detail)
                .Include(o => o.Images)
                .ToList();
            
            int year = int.Parse(param3);

            foreach(var car in cars)
            {
                if (car.Make.Equals(param1) && car.Model.Equals(param2) && car.Year == year)
                {
                    return View(car);
                }
            }

            return View("Error");
        }

    }
}
