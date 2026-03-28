using BulletinBoard.Core.Application.Dtos;
using BulletinBoard.Core.Application.Interfaces;
using BulletinBoard.Core.Application.Mappers;
using BulletinBoard.Core.Domain.Enums;

namespace BulletinBoard.Core.Application.Services
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

            var entity = dto.ToEntity();
            return await _repository.CreateAsync(entity);
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
            var entity = dto.ToEntity();
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id, string? currentUserId = null)
        {
            await _repository.DeleteAsync(id);
        }
    }
}