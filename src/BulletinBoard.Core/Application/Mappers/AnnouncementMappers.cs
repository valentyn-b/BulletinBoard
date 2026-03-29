using BulletinBoard.Core.Domain.Entities;
using BulletinBoard.Core.Application.Dtos;

namespace BulletinBoard.Core.Application.Mappers
{
    public static class AnnouncementMappers
    {
        public static Announcement ToEntity(this CreateAnnouncementDto dto)
        {
            return new Announcement
            {
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category.Value,
                SubCategory = dto.SubCategory.Value,
                AuthorId = dto.AuthorId,
                Status = true,
                CreatedDate = DateTime.UtcNow
            };
        }

        public static Announcement ToEntity(this UpdateAnnouncementDto dto)
        {
            return new Announcement
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category.Value,
                SubCategory = dto.SubCategory.Value,
                Status = dto.Status
            };
        }
    }
}