using EldenRingMVC.Data;
using EldenRingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EldenRingMVC.Controllers
{
    public class LoreController : Controller
    {
        private readonly AppDbContext _db;

        public LoreController(AppDbContext db)
        {
            _db = db;
        }

        // GET /Lore — full timeline
        public async Task<IActionResult> Index()
        {
            var entries = await _db.LoreEntries
                .OrderBy(l => l.Order)
                .ToListAsync();
            return View(entries);
        }

        // GET /Lore/Details/2
        public async Task<IActionResult> Details(int id)
        {
            var entry = await _db.LoreEntries.FindAsync(id);
            if (entry == null) return NotFound();
            return View(entry);
        }

        // GET /Lore/Category/Age
        public async Task<IActionResult> Category(string id)
        {
            var entries = await _db.LoreEntries
                .Where(l => l.Category == id)
                .OrderBy(l => l.Order)
                .ToListAsync();
            ViewBag.Category = id;
            return View(entries);
        }
    }
}
