using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Data;
using projekt.Models;
using System;
using System.Linq;

namespace projekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Pobierz wszystkie auta bez filtrowania po statusie
            var auta = _context.Samochody.ToList();

            return View(auta);
        }

        public IActionResult Szczegoly(int id)
        {
            var auto = _context.Samochody
                .Include(a => a.Wypozyczenia)
                .FirstOrDefault(c => c.Id == id);

            if (auto == null)
                return NotFound();

            return View(auto);
        }

        [HttpPost]
        public IActionResult PotwierdzWypozyczenie(
            int id, int dni,
            string imie, string nazwisko,
            string email, string telefon,
            string wiek18)
        {
            var auto = _context.Samochody
                .Include(c => c.Wypozyczenia)
                .FirstOrDefault(c => c.Id == id);

            if (auto == null)
                return RedirectToAction("Index");

            if (wiek18 != "on")
            {
                ModelState.AddModelError("", "Musisz mieć ukończone 18 lat.");
                return View("Szczegoly", auto);
            }

            if (!ModelState.IsValid)
                return View("Szczegoly", auto);

            if (auto.Status != StatusSamochodu.Dostepny)
            {
                ModelState.AddModelError("", "Samochód jest obecnie niedostępny.");
                return View("Szczegoly", auto);
            }

            // Sprawdź, czy klient już istnieje (po emailu)
            var klient = _context.Klienci.FirstOrDefault(k => k.Email == email);
            if (klient == null)
            {
                klient = new Klient
                {
                    Imie = imie,
                    Nazwisko = nazwisko,
                    Email = email,
                    Telefon = telefon
                };
                _context.Klienci.Add(klient);
                _context.SaveChanges(); // zapisujemy klienta, by mieć jego Id
            }

            decimal cena = auto.CenaZaDzien * dni;

            // Rabaty
            if (dni >= 30)
                cena *= 0.85m;
            else if (dni >= 7)
                cena *= 0.90m;

            var wypozyczenie = new Wypozyczenie
            {
                CarId = auto.Id,
                KlientId = klient.Id,
                DataOd = DateTime.Now,
                DataDo = DateTime.Now.AddDays(dni),
                CenaCalkowita = cena
            };

            _context.Wypozyczenia.Add(wypozyczenie);

            // Zmień status samochodu na wypożyczony
            auto.Status = StatusSamochodu.Wypozyczony;

            _context.SaveChanges();

            ViewBag.Cena = cena;
            ViewBag.Dni = dni;

            return View("Potwierdzenie", auto);
        }
    }
}