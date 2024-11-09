using AnimalAidPlatform.API.Data;
using AnimalAidPlatform.API.Models.DTO.Report;
using AnimalAidPlatform.API.Models.Enums;
using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AnimalAidPlatform.API.Repositories.Implementation
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllAsync()
        {
            return await _context.Reports
                .Include(r => r.FeedPost)
                .Include(r => r.Reporter)
                .ToListAsync();
        }

        public async Task<Report> GetByIdAsync(int id)
        {
            return await _context.Reports
                .Include(r => r.FeedPost)
                .Include(r => r.Reporter)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var report = await GetByIdAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ResolveReportAsync(int id, ResolveReportDTO resolveReportDto)
        {
            var report = await GetByIdAsync(id);
            if (report == null) throw new ArgumentException("A jelentés nem található.");

            report.IsResolved = resolveReportDto.IsResolved;
            report.AdminResponse = resolveReportDto.AdminResponse;

            if (resolveReportDto.IsResolved)
            {
                switch (resolveReportDto.reportAction)
                {
                    case ReportAction.Delete:
                        var feedPost = await _context.FeedPosts.FindAsync(report.FeedPostId);
                        if (feedPost != null)
                        {
                            _context.FeedPosts.Remove(feedPost);
                        }
                        break;

                    case ReportAction.NoticeCreator:
                        //TODO
                        break;

                    case ReportAction.None:
                        // Nincs további akció
                        break;
                }
            }

            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Report>> GetReportsByFeedPostIdAsync(int feedPostId)
        {
            return await _context.Reports
                .Where(report => report.FeedPostId == feedPostId)
                .Where(report => !report.IsResolved)
                .ToListAsync();
        }
    }
}
