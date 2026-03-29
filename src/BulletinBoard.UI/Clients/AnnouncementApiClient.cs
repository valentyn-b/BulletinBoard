using System.Text.Json;
using System.Text.Json.Serialization;
using BulletinBoard.UI.Interfaces;
using BulletinBoard.UI.Models;
using BulletinBoard.UI.Models.Enums;

namespace BulletinBoard.UI.Clients
{
    public class AnnouncementApiClient : IAnnouncementApiClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseEndpoint = "api/announcements";

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public AnnouncementApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AnnouncementViewModel>> GetAllAsync(Category? category = null, SubCategory? subCategory = null)
        {
            var queryParams = new List<string>();

            if (category.HasValue) queryParams.Add($"category={category.Value}");
            if (subCategory.HasValue) queryParams.Add($"subCategory={subCategory.Value}");

            var url = queryParams.Any()
                ? $"{BaseEndpoint}?{string.Join("&", queryParams)}"
                : BaseEndpoint;

            return await _httpClient.GetFromJsonAsync<IEnumerable<AnnouncementViewModel>>(url, JsonOptions)
                   ?? Array.Empty<AnnouncementViewModel>();
        }

        public async Task<AnnouncementViewModel?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AnnouncementViewModel>($"{BaseEndpoint}/{id}", JsonOptions);
        }

        public async Task<IEnumerable<AnnouncementViewModel>> GetMyAnnouncementsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<AnnouncementViewModel>>($"{BaseEndpoint}/my", JsonOptions)
                   ?? Array.Empty<AnnouncementViewModel>();
        }

        public async Task CreateAsync(CreateAnnouncementViewModel dto)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, dto, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, UpdateAnnouncementViewModel dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{id}", dto, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}