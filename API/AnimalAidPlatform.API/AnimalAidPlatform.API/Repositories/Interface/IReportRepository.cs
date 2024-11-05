using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Models.DTO.Report;

namespace AnimalAidPlatform.API.Repositories.Interface
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetAllAsync();
        Task<Report> GetByIdAsync(int id);
        Task AddAsync(Report report);
        Task UpdateAsync(Report report);
        Task DeleteAsync(int id);
        Task ResolveReportAsync(int id, ResolveReportDTO resolveReportDto);
        Task<IEnumerable<Report>> GetReportsByFeedPostIdAsync(int feedPostId);
    }

}
