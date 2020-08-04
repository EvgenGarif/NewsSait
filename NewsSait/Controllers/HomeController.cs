using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using NewsSait.Models;

namespace NewsSait.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Name = User.Identity.Name;
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            _logger.LogInformation(@"Entrance to the site by name{0}", User.Identity.Name);
            var http = new HttpClient();
            var response = await http.GetAsync("https://newsapi.org/v2/top-headlines?country=ru&apiKey=f98f3c8defe74c24a7fcd4834c97326b");
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<NewsResponse>(result);
            return View(data.articles);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string searchNews)
        {
            ViewBag.Name = User.Identity.Name;
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            _logger.LogInformation(@"The search news by keyword{0}", searchNews);
            var http = new HttpClient();
            var search = @"https://newsapi.org/v2/top-headlines?country=ru&q=" + searchNews + @"&apiKey=f98f3c8defe74c24a7fcd4834c97326b";
            var response = await http.GetAsync(search);
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<NewsResponse>(result);
            return View(data.articles);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Privacy()
        {
            _logger.LogInformation(@"Administrator login{0}", User.Identity.Name);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("Error");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
