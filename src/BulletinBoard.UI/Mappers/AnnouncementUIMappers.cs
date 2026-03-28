using BulletinBoard.UI.Models.Dtos;
namespace BulletinBoard.UI.Mappers
{
    public static class AnnouncementUIMappers
    {
        public static UpdateAnnouncementDto ToUpdateDto(this AnnouncementDto dto)
        {
            return new UpdateAnnouncementDto
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                SubCategory = dto.SubCategory,
                Status = dto.Status
            };
        }
    }
}