using Microsoft.AspNetCore.Mvc;
using projekt.Models;
using System.Collections.Generic;

namespace projekt.Controllers
{
    public class HomeController : Controller
    {
        private static List<Car> samochody = new List<Car>
{
    new Car {
        Id = 1, Marka = "Toyota", Model = "Corolla", CenaZaDzien = 120, Dostepny = true,
        Silnik = "1.8L I4", Spalanie = "6.5 L/100 km", MocKM = 140, PrzyspieszenieSek = 9.5m, Naped = "FWD"
    },

    new Car {
        Id = 2, Marka = "BMW", Model = "M3", CenaZaDzien = 350, Dostepny = true,
        Silnik = "3.0L I6 Turbo", Spalanie = "9.0 L/100 km", MocKM = 480, PrzyspieszenieSek = 4.1m, Naped = "RWD"
    },

    new Car {
        Id = 3, Marka = "Volkswagen", Model = "Golf GTI", CenaZaDzien = 180, Dostepny = true,
        Silnik = "2.0L I4 Turbo", Spalanie = "7.5 L/100 km", MocKM = 220, PrzyspieszenieSek = 6.4m, Naped = "FWD"
    },

    new Car {
        Id = 4, Marka = "Porsche", Model = "911 Carrera", CenaZaDzien = 600, Dostepny = true,
        Silnik = "3.0L Boxer 6", Spalanie = "10.2 L/100 km", MocKM = 385, PrzyspieszenieSek = 4.2m, Naped = "RWD"
    },

    new Car {
        Id = 5, Marka = "Ferrari", Model = "488 GTB", CenaZaDzien = 1200, Dostepny = true,
        Silnik = "3.9L V8 Twin-Turbo", Spalanie = "13.1 L/100 km", MocKM = 670, PrzyspieszenieSek = 3.0m, Naped = "RWD"
    },

    new Car {
        Id = 6, Marka = "Lamborghini", Model = "Huracán", CenaZaDzien = 1500, Dostepny = true,
        Silnik = "5.2L V10", Spalanie = "12.5 L/100 km", MocKM = 610, PrzyspieszenieSek = 2.9m, Naped = "AWD"
    },

    new Car {
        Id = 7, Marka = "Audi", Model = "R8", CenaZaDzien = 1300, Dostepny = true,
        Silnik = "5.2L V10", Spalanie = "13.0 L/100 km", MocKM = 540, PrzyspieszenieSek = 3.4m, Naped = "AWD"
    },

    new Car {
        Id = 8, Marka = "McLaren", Model = "720S", CenaZaDzien = 1600, Dostepny = true,
        Silnik = "4.0L V8 Twin-Turbo", Spalanie = "11.9 L/100 km", MocKM = 710, PrzyspieszenieSek = 2.9m, Naped = "RWD"
    },

    new Car {
        Id = 9, Marka = "Nissan", Model = "GT-R", CenaZaDzien = 900, Dostepny = true,
        Silnik = "3.8L V6 Twin-Turbo", Spalanie = "11.2 L/100 km", MocKM = 565, PrzyspieszenieSek = 2.9m, Naped = "AWD"
    },

    new Car {
        Id = 10, Marka = "Chevrolet", Model = "Corvette C8", CenaZaDzien = 1000, Dostepny = true,
        Silnik = "6.2L V8", Spalanie = "13.7 L/100 km", MocKM = 495, PrzyspieszenieSek = 2.8m, Naped = "RWD"
    }
};


        public IActionResult Index()
        {
            return View(samochody);
        }

        public IActionResult Szczegoly(int id)
        {
            var auto = samochody.Find(c => c.Id == id);
            if (auto == null) return RedirectToAction("Index");
            return View(auto);
        }

        [HttpPost]
        public IActionResult PotwierdzWypozyczenie(int id, int dni)
        {
            var auto = samochody.Find(c => c.Id == id);
            if (auto == null || !auto.Dostepny) return RedirectToAction("Index");

            decimal cena = auto.CenaZaDzien * dni;

            // Zni¿ki
            if (dni >= 30)
                cena *= 0.85m; // 15% rabatu
            else if (dni >= 7)
                cena *= 0.90m; // 10% rabatu

            auto.Dostepny = false;
            auto.Wypozyczenia.Add(new Wypozyczenie
            {
                Dni = dni,
                CenaCalkowita = cena,
                Data = DateTime.Now
            });

            ViewBag.Cena = cena;
            ViewBag.Dni = dni;
            return View("Potwierdzenie", auto);
        }

        public IActionResult Zwroc(int id)
        {
            var auto = samochody.Find(c => c.Id == id);
            if (auto != null)
            {
                auto.Dostepny = true;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Historia()
        {
            return View(samochody);
        }
    }
}
