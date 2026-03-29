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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateAnnouncementDto dto)
        {
            var authorId = GetCurrentUserId();
            if (string.IsNullOrEmpty(authorId)) return Unauthorized();

            var newId = await _service.CreateAsync(dto, authorId);
            return CreatedAtAction(nameof(GetById), new { id = newId }, new { Id = newId });
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<AnnouncementDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] Category? category = null, [FromQuery] SubCategory? subCategory = null)
        {
            var announcements = await _service.GetAllAsync(category, subCategory);
            return Ok(announcements);
        }

        [HttpGet("my")]
        [ProducesResponseType(typeof(IEnumerable<AnnouncementDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMyAnnouncements()
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var myAnnouncements = await _service.GetByUserIdAsync(userId);
            return Ok(myAnnouncements);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AnnouncementDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var announcement = await _service.GetByIdAsync(id);
            if (announcement == null) return NotFound();
            return Ok(announcement);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAnnouncementDto dto)
        {
            if (id != dto.Id) return BadRequest("ID in URL and body must match.");

            var currentUserId = GetCurrentUserId();
            if (string.IsNullOrEmpty(currentUserId)) return Unauthorized();

            try
            {
                await _service.UpdateAsync(dto, currentUserId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserId = GetCurrentUserId();
            if (string.IsNullOrEmpty(currentUserId)) return Unauthorized();

            try
            {
                await _service.DeleteAsync(id, currentUserId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}