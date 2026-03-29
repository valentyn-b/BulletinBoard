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

        public async Task<IEnumerable<AnnouncementDto>> GetByUserIdAsync(string userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }

        public async Task<AnnouncementDto?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UpdateAnnouncementDto dto, string currentUserId)
        {
            var existing = await _repository.GetByIdAsync(dto.Id);

            if (existing == null)
            {
                throw new KeyNotFoundException($"Announcement with ID {dto.Id} not found.");
            }

            if (existing.AuthorId != currentUserId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this announcement.");
            }

            var entityToUpdate = dto.ToEntity();
            entityToUpdate.AuthorId = existing.AuthorId;
            entityToUpdate.CreatedDate = existing.CreatedDate;

            await _repository.UpdateAsync(entityToUpdate);
        }

        public async Task DeleteAsync(int id, string currentUserId)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
            {
                throw new KeyNotFoundException($"Announcement with ID {id} not found.");
            }

            if (existing.AuthorId != currentUserId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this announcement.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}