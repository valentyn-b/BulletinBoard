using BulletinBoard.Core.Application.Dtos;
using BulletinBoard.Core.Application.Interfaces;
using BulletinBoard.Core.Domain.Entities;
using BulletinBoard.Core.Domain.Enums;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BulletinBoard.Infrastructure.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly string _connectionString;

        public AnnouncementRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<int> CreateAsync(Announcement entity)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();

            parameters.Add("Title", entity.Title);
            parameters.Add("Description", entity.Description);
            parameters.Add("Category", (int)entity.Category);
            parameters.Add("SubCategory", (int)entity.SubCategory);
            parameters.Add("AuthorId", entity.AuthorId);

            parameters.Add("NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "sp_CreateAnnouncement",
                parameters,
                commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("NewId");
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAsync(Category? category = null, SubCategory? subCategory = null)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new { Category = (int?)category, SubCategory = (int?)subCategory };

            return await connection.QueryAsync<AnnouncementDto>(
                "sp_GetAnnouncements",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<AnnouncementDto>> GetByUserIdAsync(string userId)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<AnnouncementDto>(
                "sp_GetAnnouncementsByUserId",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<AnnouncementDto?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QuerySingleOrDefaultAsync<AnnouncementDto>(
                "sp_GetAnnouncementById",
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(Announcement entity)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new
            {
                entity.Id,
                entity.Title,
                entity.Description,
                Category = (int)entity.Category,
                SubCategory = (int)entity.SubCategory,
                entity.Status
            };

            await connection.ExecuteAsync(
                "sp_UpdateAnnouncement",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync(
                "sp_DeleteAnnouncement",
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}