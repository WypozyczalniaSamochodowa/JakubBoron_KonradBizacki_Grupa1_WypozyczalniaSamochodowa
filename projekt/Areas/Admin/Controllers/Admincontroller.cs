using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Data;
using projekt.Models;

namespace projekt.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;

        public AdminController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (!User.IsInRole("Admin"))
            {
                Console.WriteLine("Użytkownik nie ma roli Admin!");
            }
            var viewModel = new AdminViewModel
            {
                Samochody = _db.Samochody.ToList(),
                Wypozyczenia = _db.Wypozyczenia
                    .Include(w => w.Car)
                    .Include(w => w.Klient)
                    .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, string status)
        {
            Console.WriteLine($"UpdateStatus: id={id}, status={status}");

            var car = _db.Samochody.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                Console.WriteLine($"Nie znaleziono samochodu o id={id}");
                return NotFound();
            }

            bool parseSuccess = Enum.TryParse<StatusSamochodu>(status, out var newStatus);
            if (!parseSuccess)
            {
                Console.WriteLine($"Niepoprawny status: {status}");
                return BadRequest();
            }

            car.Status = newStatus;

            _db.Entry(car).State = EntityState.Modified;
            _db.SaveChanges();

            Console.WriteLine($"Zmieniono status auta {car.Marka} {car.Model} na {car.Status}");

            TempData["msg"] = $"Status samochodu {car.Marka} {car.Model} został zmieniony na {car.Status}";

            return RedirectToAction("Index");
        }

    }
}
