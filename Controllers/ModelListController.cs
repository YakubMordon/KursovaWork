using KursovaWork.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KursovaWork.Controllers
{
    public class ModelListController : Controller
    {
        private readonly CarSaleContext _context;

        private readonly ILogger<ModelListController> _logger;

        public ModelListController(CarSaleContext context, ILogger<ModelListController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult ModelList()
        {
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;
            var cars = _context.Cars
            .Include(o => o.Detail)
            .Include(o => o.Images)
            .ToList();

            return View(cars);
        }

        public IActionResult DiscountList()
        {
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;
            return View("~/Views/ModelList/ModelList.cshtml");
        }
    }
}
