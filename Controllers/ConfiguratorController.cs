﻿using KursovaWork.Entity.Entities.Car;
using KursovaWork.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using KursovaWork.Entity.Entities;

namespace KursovaWork.Controllers
{
    public class ConfiguratorController : Controller
    {
        private readonly CarSaleContext _context;

        private readonly ILogger<ConfiguratorController> _logger;
        public static ConfiguratorOptions _options { get; set; }

        private static string[] param = new string[3];

        public ConfiguratorController(CarSaleContext context, ILogger<ConfiguratorController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Configurator(string param1, string param2, string param3)
        {
            ViewBag.IsLoggedIn = HttpContext.User.Identity.IsAuthenticated ? true : false;

            List<CarInfo> cars = _context.Cars
                .Include(o => o.Detail)
                .Include(o => o.Images)
                .ToList();

            param[0] = param1;
            param[1] = param2;
            param[2] = param3;

            int year = int.Parse(param3);

            foreach (var car in cars)
            {
                if (car.Make.Equals(param1) && car.Model.Equals(param2) && car.Year == year)
                {
                    return View(car);
                }
            }

            return View("Error");
        }

        public IActionResult Submit(string color, string transmission, string fuelType)
        {
            _options = new ConfiguratorOptions();
            _options.Color = color;
            _options.FuelType = fuelType;
            _options.Transmission = transmission;

            return RedirectToAction("Payment", "Payment", new
            {
                param1 = param[0],
                param2 = param[1],
                param3 = param[2]
            });
        }
    }
}