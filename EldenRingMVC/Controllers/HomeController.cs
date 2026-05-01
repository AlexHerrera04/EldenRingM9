using EldenRingMVC.Data;
using EldenRingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EldenRingMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(AppDbContext db, IHttpClientFactory httpClientFactory)
        {
            _db = db;
            _httpClientFactory = httpClientFactory;
        }

        // GET /
        public async Task<IActionResult> Index()
        {
            // Consumim l'API externa d'Elden Ring per obtenir un boss aleatori com a "boss del dia"
            ApiBoss? featuredApiBoss = null;
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(5);
                client.DefaultRequestHeaders.Add("User-Agent", "EldenRingMVC/1.0");
                var json = await client.GetStringAsync("https://eldenring.fanapis.com/api/bosses?limit=50");
                var result = JsonConvert.DeserializeObject<ApiBossResponse>(json);
                if (result?.Data != null && result.Data.Any())
                {
                    var rng = new Random();
                    featuredApiBoss = result.Data[rng.Next(result.Data.Count)];
                }
            }
            catch { /* No bloqueja si l'API no respon */ }

            // Top 3 bosses de la BD local
            var topBosses = await _db.Bosses
                .OrderByDescending(b => b.Difficulty)
                .Take(3)
                .ToListAsync();

            ViewBag.FeaturedApiBoss = featuredApiBoss;
            ViewBag.TopBosses = topBosses;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
