using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Data;
using projekt.Models;
using Rotativa.AspNetCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            var auta = _context.Samochody.ToList();
            return View(auta);
        }

        public async Task<IActionResult> Szczegoly(int id)
        {
            var car = await _context.Samochody
                .Include(c => c.Wypozyczenia)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            var isAdmin = User.IsInRole("Admin");
            var isAuthenticated = User.Identity.IsAuthenticated;

            ViewBag.IsAdmin = isAdmin;
            ViewBag.IsAuthenticated = isAuthenticated;

            if (car.Status != StatusSamochodu.Dostepny)
            {
                if (!isAuthenticated || !isAdmin)
                {
                    return Forbid();
                }
            }

            return View(car);
        }

        [HttpPost]
        [Authorize]
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
                _context.SaveChanges();
            }

            decimal cena = auto.CenaZaDzien * dni;

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
            auto.Status = StatusSamochodu.Wypozyczony;
            _context.SaveChanges();

            ViewBag.Cena = cena;
            ViewBag.Dni = dni;
            ViewBag.WypozyczenieId = wypozyczenie.Id;

            return View("Potwierdzenie", auto);
        }

        public IActionResult GenerujFakture(int wypozyczenieId)
        {
            var wypozyczenie = _context.Wypozyczenia
                .Include(w => w.Car)
                .Include(w => w.Klient)
                .FirstOrDefault(w => w.Id == wypozyczenieId);

            if (wypozyczenie == null)
                return NotFound();

            return new ViewAsPdf("Faktura", wypozyczenie)
            {
                FileName = $"Faktura_{wypozyczenie.Id}.pdf"
            };
        }
    }
}
