using BulletinBoard.Core.Dtos;
using BulletinBoard.Core.Enums;
using BulletinBoard.Core.Interfaces;
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

        public async Task<int> CreateAsync(CreateAnnouncementDto dto)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("Title", dto.Title);
            parameters.Add("Description", dto.Description);
            parameters.Add("Category", (int)dto.Category);
            parameters.Add("SubCategory", (int)dto.SubCategory);
            parameters.Add("AuthorId", dto.AuthorId);

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

        public async Task<AnnouncementDto?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QuerySingleOrDefaultAsync<AnnouncementDto>(
                "sp_GetAnnouncementById",
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(UpdateAnnouncementDto dto)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Category = (int)dto.Category,
                SubCategory = (int)dto.SubCategory,
                Status = dto.Status
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
