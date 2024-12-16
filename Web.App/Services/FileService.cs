using Dybi.Library;
using Web.Models.SeedWork;

namespace Web.App.Services
{
    public class FileService : IFileService
    {
        private readonly ApiCaller _httpClient;

        public FileService(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FileData>> Get()
        {
            var files = await _httpClient.GetFromJsonAsync<List<FileData>>("/api/files");
            return files;
        }

        public async Task<bool> Upload(List<FileData> filesBase64)
        {
            var msg = await _httpClient.PostAsJsonAsync("/api/files", filesBase64, CancellationToken.None);
            return msg.IsSuccessStatusCode;
        }

        public async Task<bool> Upload(FileData file)
        {
            var msg = await _httpClient.PostAsJsonAsync("/api/files", file, CancellationToken.None);
            return msg.IsSuccessStatusCode;
        }
        public async Task<bool> Delete(string fileName)
        {
            var msg = await _httpClient.DeleteAsync($"/api/files/{fileName}", CancellationToken.None);
            return msg.IsSuccessStatusCode;
        }
    }
}
