using BulletinBoard.Core.Application.Dtos;
using BulletinBoard.Core.Application.Interfaces;
using BulletinBoard.Core.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulletinBoard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _service;

        public AnnouncementsController(IAnnouncementService service)
        {
            _service = service;
        }

        private string? GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAnnouncementDto dto)
        {
            var authorId = GetCurrentUserId();

            var newId = await _service.CreateAsync(dto, authorId);

            // 201
            return CreatedAtAction(nameof(GetById), new { id = newId }, new { Id = newId });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] Category? category = null, [FromQuery] SubCategory? subCategory = null)
        {
            var announcements = await _service.GetAllAsync(category, subCategory);

            // 200
            return Ok(announcements);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyAnnouncements()
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var all = await _service.GetAllAsync();
            var myAnnouncements = all.Where(a => a.AuthorId == userId);

            return Ok(myAnnouncements);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var announcement = await _service.GetByIdAsync(id);

            if (announcement == null)
            {
                // 404
                return NotFound();
            }

            // 200
            return Ok(announcement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAnnouncementDto dto)
        {
            if (id != dto.Id)
            {
                // 400
                return BadRequest("ID in URL and body must match.");
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
            {
                // 404
                return NotFound();
            }

            var currentUserId = GetCurrentUserId();
            if (existing.AuthorId != currentUserId)
            {
                // 403
                return Forbid();
            }

            await _service.UpdateAsync(dto);

            // 204
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
            {
                // 404
                return NotFound();
            }

            var currentUserId = GetCurrentUserId();
            if (existing.AuthorId != currentUserId)
            {
                // 403
                return Forbid();
            }

            await _service.DeleteAsync(id);

            // 204
            return NoContent();
        }
    }
}
