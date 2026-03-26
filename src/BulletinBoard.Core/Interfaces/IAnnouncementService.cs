using BulletinBoard.Core.Dtos;
using BulletinBoard.Core.Enums;

namespace BulletinBoard.Core.Interfaces
{
    public interface IAnnouncementService
    {
        Task<int> CreateAsync(CreateAnnouncementDto dto, string? authorId = null);
        Task<IEnumerable<AnnouncementDto>> GetAllAsync(Category? category = null, SubCategory? subCategory = null);
        Task<AnnouncementDto?> GetByIdAsync(int id);
        Task UpdateAsync(UpdateAnnouncementDto dto, string? currentUserId = null);
        Task DeleteAsync(int id, string? currentUserId = null);
    }
}
