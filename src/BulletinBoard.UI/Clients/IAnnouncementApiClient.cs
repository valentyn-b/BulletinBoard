using BulletinBoard.UI.Models.Dtos;
using BulletinBoard.UI.Models.Enums;

namespace BulletinBoard.UI.Clients
{
    public interface IAnnouncementApiClient
    {
        Task<IEnumerable<AnnouncementDto>> GetAllAsync(Category? category = null, SubCategory? subCategory = null);
        Task<AnnouncementDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateAnnouncementDto dto);
        Task UpdateAsync(int id, UpdateAnnouncementDto dto);
        Task DeleteAsync(int id);
    }
}
