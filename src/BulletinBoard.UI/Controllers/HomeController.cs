using BulletinBoard.UI.Clients;
using BulletinBoard.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulletinBoard.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnnouncementApiClient _apiClient;

        public HomeController(IAnnouncementApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Index()
        {
            var announcements = await _apiClient.GetAllAsync();

            return View(announcements);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
