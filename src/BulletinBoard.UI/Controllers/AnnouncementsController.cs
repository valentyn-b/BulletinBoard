using BulletinBoard.UI.Clients;
using BulletinBoard.UI.Mappers;
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

        // GET: /Announcements/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var announcement = await _apiClient.GetByIdAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }

            var updateDto = announcement.ToUpdateDto();

            return View(updateDto);
        }

        // POST: /Announcements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateAnnouncementDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _apiClient.UpdateAsync(id, dto);

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /Announcements/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var announcement = await _apiClient.GetByIdAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // POST: /Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiClient.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
