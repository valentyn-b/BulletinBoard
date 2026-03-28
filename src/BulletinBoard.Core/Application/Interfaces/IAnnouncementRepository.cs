using BulletinBoard.Core.Application.Dtos;
using BulletinBoard.Core.Domain.Entities;
using BulletinBoard.Core.Domain.Enums;

namespace BulletinBoard.Core.Application.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<AnnouncementDto>> GetAllAsync(Category? category = null, SubCategory? subCategory = null);

        Task<AnnouncementDto?> GetByIdAsync(int id);

        Task<int> CreateAsync(Announcement entity);

        Task UpdateAsync(Announcement entity);

        Task DeleteAsync(int id);
    }
}
