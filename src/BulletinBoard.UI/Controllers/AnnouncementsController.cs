using BulletinBoard.UI.Clients;
using BulletinBoard.UI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.UI.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly IAnnouncementApiClient _apiClient;

        public AnnouncementsController(IAnnouncementApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        // GET: /Announcements
        public async Task<IActionResult> Index()
        {
            var announcements = await _apiClient.GetAllAsync();
            return View(announcements);
        }

        // GET: /Announcements/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var announcement = await _apiClient.GetByIdAsync(id);

            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // GET: /Announcements/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Announcements/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateAnnouncementDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _apiClient.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
    }
}
