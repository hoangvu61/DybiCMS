using Web.Models.SeedWork;

namespace Web.App.Services
{
    public interface IFileService
    {
        Task<List<FileData>> Get();
        Task<bool> Upload(List<FileData> filesBase64);
        Task<bool> Upload(FileData file);
        Task<bool> Delete(string fileName);
    }
}
