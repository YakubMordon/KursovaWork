using KursovaWork.Entity;
using KursovaWork.Entity.Entities.Car;
using KursovaWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace KursovaWork.Controllers
{
    public class ModelListController : Controller
    {
        private readonly CarSaleContext _context;

        private readonly ILogger<ModelListController> _logger;

        private static List<CarInfo> _curList;

        private static string _filter = "";

        public ModelListController(CarSaleContext context, ILogger<ModelListController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult ModelList()
        {
            _curList = _context.Cars
            .Include(o => o.Detail)
            .Include(o => o.Images)
            .ToList();

            var model = new FilterViewModel();
            FilterViewModel.origCars = _curList;
            model.cars = _curList;

            return View(model);
        }

        public IActionResult SortByAlphabet()
        {
            _curList = _curList.OrderByDescending(o => (o.Make + o.Model)).ToList();

            var model = new FilterViewModel();
            model.cars = _curList;

            return View("~/Views/ModelList/ModelList.cshtml", model);
        }

        public IActionResult SortByPrice(string param1)
        {
            if (param1.Equals("cheap"))
            {
                _curList = _curList.OrderBy(o => o.Price).ToList();
            }
            else
            {
                _curList = _curList.OrderByDescending(o => o.Price).ToList();
            }

            var model = new FilterViewModel();
            model.cars = _curList;

            return View("~/Views/ModelList/ModelList.cshtml", model);
        }

        public IActionResult SortByNovelty()
        {
            _curList = _curList.OrderByDescending(o => o.Year).ToList();

            var model = new FilterViewModel();
            model.cars = _curList;
            return View("~/Views/ModelList/ModelList.cshtml", model);
        }

        public IActionResult ApplyFilters(FilterViewModel filter)
        {
            var filteredCars = _context.Cars
                .Include(o => o.Detail)
                .Include(o => o.Images)
                .ToList();

            if (filter.PriceFrom.HasValue)
            {
                filteredCars = filteredCars.Where(c => c.Price >= filter.PriceFrom.Value).ToList();
            }

            if (filter.PriceTo.HasValue)
            {
                filteredCars = filteredCars.Where(c => c.Price <= filter.PriceTo.Value).ToList();
            }

            if (filter.YearFrom.HasValue)
            {
                filteredCars = filteredCars.Where(c => c.Year >= filter.YearFrom.Value).ToList();
            }

            if (filter.YearTo.HasValue)
            {
                filteredCars = filteredCars.Where(c => c.Year <= filter.YearTo.Value).ToList();
            }

            if (filter.SelectedFuelTypes != null && filter.SelectedFuelTypes.Any())
            {
                filteredCars = filteredCars.Where(c => filter.SelectedFuelTypes.Contains(c.Detail.FuelType)).ToList();
            }

            if (filter.SelectedTransmissionTypes != null && filter.SelectedTransmissionTypes.Any())
            {
                filteredCars = filteredCars.Where(c => filter.SelectedTransmissionTypes.Contains(c.Detail.Transmission)).ToList();
            }

            if (filter.SelectedMakes != null && filter.SelectedMakes.Any())
            {
                filteredCars = filteredCars.Where(c => filter.SelectedMakes.Contains(c.Make)).ToList();
            }


            _curList = filteredCars.ToList();

            filter.cars = _curList;

            return View("~/Views/ModelList/ModelList.cshtml", filter);
        }

    }
}
