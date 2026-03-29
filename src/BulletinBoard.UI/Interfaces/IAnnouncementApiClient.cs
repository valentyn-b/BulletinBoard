using BulletinBoard.UI.Models;
using BulletinBoard.UI.Models.Enums;

namespace BulletinBoard.UI.Interfaces
{
    public interface IAnnouncementApiClient
    {
        Task<IEnumerable<AnnouncementViewModel>> GetAllAsync(Category? category = null, SubCategory? subCategory = null);
        Task<AnnouncementViewModel?> GetByIdAsync(int id);
        Task<IEnumerable<AnnouncementViewModel>> GetMyAnnouncementsAsync();
        Task CreateAsync(CreateAnnouncementViewModel dto);
        Task UpdateAsync(int id, UpdateAnnouncementViewModel dto);
        Task DeleteAsync(int id);
    }
}
