using BulletinBoard.Core.Dtos;
using BulletinBoard.Core.Enums;

namespace BulletinBoard.Core.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<int> CreateAsync(CreateAnnouncementDto dto);

        Task<IEnumerable<AnnouncementDto>> GetAllAsync(Category? category = null, SubCategory? subCategory = null);

        Task<AnnouncementDto?> GetByIdAsync(int id);

        Task UpdateAsync(UpdateAnnouncementDto dto);

        Task DeleteAsync(int id);
    }
}
