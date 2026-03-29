using BulletinBoard.UI.Interfaces;
using BulletinBoard.UI.Models;
using BulletinBoard.UI.Models.Enums;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Index(Category? category, SubCategory? subCategory)
        {
            var announcements = await _apiClient.GetAllAsync(category, subCategory);

            ViewBag.CurrentCategory = category;
            ViewBag.CurrentSubCategory = subCategory;

            return View(announcements);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MyAnnouncements()
        {
            var myAnnouncements = await _apiClient.GetMyAnnouncementsAsync();
            return View(myAnnouncements);
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
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Announcements/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAnnouncementViewModel dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _apiClient.CreateAsync(dto);

            return RedirectToAction(nameof(MyAnnouncements));
        }

        // GET: /Announcements/Edit/5
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var announcement = await _apiClient.GetByIdAsync(id);

            if (announcement == null)
            {
                return NotFound();
            }

            var updateVm = new UpdateAnnouncementViewModel
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Description = announcement.Description,
                Category = announcement.Category,
                SubCategory = announcement.SubCategory,
                Status = announcement.Status
            };

            return View(updateVm);
        }

        // POST: /Announcements/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateAnnouncementViewModel dto)
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
        [Authorize]
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
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiClient.DeleteAsync(id);

            return RedirectToAction(nameof(MyAnnouncements));
        }
    }
}