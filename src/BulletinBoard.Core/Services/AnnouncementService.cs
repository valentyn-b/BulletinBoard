using BulletinBoard.Core.Dtos;
using BulletinBoard.Core.Enums;
using BulletinBoard.Core.Interfaces;

namespace BulletinBoard.Core.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _repository;

        public AnnouncementService(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(CreateAnnouncementDto dto, string? authorId = null)
        {
            dto.AuthorId = authorId;
            return await _repository.CreateAsync(dto);
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAsync(Category? category = null, SubCategory? subCategory = null)
        {
            return await _repository.GetAllAsync(category, subCategory);
        }

        public async Task<AnnouncementDto?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UpdateAnnouncementDto dto, string? currentUserId = null)
        {
            await _repository.UpdateAsync(dto);
        }

        public async Task DeleteAsync(int id, string? currentUserId = null)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
