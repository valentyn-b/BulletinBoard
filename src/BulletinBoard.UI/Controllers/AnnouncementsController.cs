using BulletinBoard.UI.Interfaces;
using BulletinBoard.UI.Mappers;
using BulletinBoard.UI.Models.Dtos;
using BulletinBoard.UI.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
            ViewBag.CategoryRules = GetCategoryRulesJson();

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
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryRules = GetCategoryRulesJson();
            return View();
        }

        // POST: /Announcements/Create
        [Authorize]
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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var announcement = await _apiClient.GetByIdAsync(id);

            if (announcement == null)
            {
                return NotFound();
            }

            var updateDto = announcement.ToUpdateDto();

            ViewBag.CategoryRules = GetCategoryRulesJson();

            return View(updateDto);
        }

        // POST: /Announcements/Edit/5
        [Authorize]
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiClient.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private string GetCategoryRulesJson()
        {
            var categoryMap = Enum.GetValues(typeof(Category)).Cast<Category>()
                .ToDictionary(
                    c => (int)c,
                    c => Enum.GetValues(typeof(SubCategory)).Cast<SubCategory>()
                             .Where(s => (int)s >= (int)c * 100 && (int)s < ((int)c + 1) * 100)
                             .Select(s => (int)s)
                             .ToList()
                );
            return JsonSerializer.Serialize(categoryMap);
        }
    }
}
