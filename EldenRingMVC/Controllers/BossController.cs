using EldenRingMVC.Data;
using EldenRingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EldenRingMVC.Controllers
{
    public class BossController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHttpClientFactory _httpClientFactory;

        public BossController(AppDbContext db, IHttpClientFactory httpClientFactory)
        {
            _db = db;
            _httpClientFactory = httpClientFactory;
        }

        // GET /Boss
        public async Task<IActionResult> Index()
        {
            var bosses = await _db.Bosses
                .OrderByDescending(b => b.Difficulty)
                .ToListAsync();
            return View(bosses);
        }

        // GET /Boss/Details/5
        // Consulta l'API externa per obtenir la imatge actualitzada del boss
        public async Task<IActionResult> Details(int id)
        {
            var boss = await _db.Bosses.FindAsync(id);
            if (boss == null) return NotFound();

            try
            {
                var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(5);
                client.DefaultRequestHeaders.Add("User-Agent", "EldenRingMVC/1.0");

                var json = await client.GetStringAsync("https://eldenring.fanapis.com/api/bosses?limit=50");
                var result = JsonConvert.DeserializeObject<ApiBossResponse>(json);

                if (result?.Data != null)
                {
                    var firstName = boss.Name.Split(',')[0].Trim().ToLower();
                    var apiBoss = result.Data.FirstOrDefault(b =>
                        b.Name.ToLower().Contains(firstName) ||
                        firstName.Contains(b.Name.ToLower().Split(' ')[0])
                    );

                    if (apiBoss != null && !string.IsNullOrEmpty(apiBoss.Image))
                    {
                        boss.ImageUrl = apiBoss.Image;
                    }
                }
            }
            catch { /* Si l'API falla, continua sense imatge */ }

            return View(boss);
        }

        // GET /Boss/ApiData
        public async Task<IActionResult> ApiData()
        {
            var apiBosses = new List<ApiBoss>();
            var apiStatus = "ok";
            var apiUrl = "https://eldenring.fanapis.com/api/bosses?limit=20";

            try
            {
                var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(8);
                client.DefaultRequestHeaders.Add("User-Agent", "EldenRingMVC/1.0");
                var json = await client.GetStringAsync(apiUrl);
                var result = JsonConvert.DeserializeObject<ApiBossResponse>(json);
                apiBosses = result?.Data ?? new List<ApiBoss>();
            }
            catch (Exception ex)
            {
                apiStatus = "error: " + ex.Message;
            }

            ViewBag.ApiStatus = apiStatus;
            ViewBag.ApiUrl = apiUrl;
            return View(apiBosses);
        }

        // GET /Boss/Ranking
        public async Task<IActionResult> Ranking()
        {
            var bosses = await _db.Bosses
                .OrderByDescending(b => b.Difficulty)
                .ToListAsync();
            return View(bosses);
        }
    }
}
