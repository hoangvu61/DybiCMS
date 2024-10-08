using Web.Models;
using Web.Models.SeedWork;

namespace Web.Admin.Services
{
    public interface IFileService
    {
        Task<List<FileData>> Get();
        Task<bool> Upload(List<FileData> filesBase64);
        Task<bool> Upload(FileData file);
        Task<bool> Delete(string fileName);

        Task<FileData> GetSiteMap();
        Task<bool> CreateSiteMap();
    }
}
