using Web.Models;

namespace Web.Backend.Services
{
    public interface ISEOApiClient
    {
        Task<List<SEOPageDto>> GetList();
        Task<SEOPageDto> GetById(Guid id);
        Task<bool> Create(SEOPageDto request);
        Task<bool> Update(SEOPageDto request);
        Task<bool> Delete(Guid id);
    }
}
