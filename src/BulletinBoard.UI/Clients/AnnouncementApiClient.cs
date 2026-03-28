using BulletinBoard.UI.Models.Dtos;
using BulletinBoard.UI.Models.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BulletinBoard.UI.Clients
{
    public class AnnouncementApiClient : IAnnouncementApiClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseEndpoint = "api/announcements";
        private readonly JsonSerializerOptions _jsonOptions;

        public AnnouncementApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAsync(Category? category = null, SubCategory? subCategory = null)
        {
            var queryParams = new List<string>();

            if (category.HasValue) queryParams.Add($"category={category.Value}");
            if (subCategory.HasValue) queryParams.Add($"subCategory={subCategory.Value}");

            var url = queryParams.Any()
                ? $"{BaseEndpoint}?{string.Join("&", queryParams)}"
                : BaseEndpoint;

            return await _httpClient.GetFromJsonAsync<IEnumerable<AnnouncementDto>>(url, _jsonOptions)
                   ?? Array.Empty<AnnouncementDto>();
        }

        public async Task<AnnouncementDto?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AnnouncementDto>($"{BaseEndpoint}/{id}");
        }

        public async Task CreateAsync(CreateAnnouncementDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, UpdateAnnouncementDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
