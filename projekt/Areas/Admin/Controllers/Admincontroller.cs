using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using projekt.Data;
using projekt.Models;  // dodaj ten using

namespace projekt.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]  // <- zabezpieczenie
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;

        public AdminController(AppDbContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            var cars = _db.Samochody.ToList();
            return View(cars);
        }

        public IActionResult Delete(int id)
        {
            var car = _db.Samochody.Find(id);
            if (car != null)
            {
                _db.Samochody.Remove(car);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}