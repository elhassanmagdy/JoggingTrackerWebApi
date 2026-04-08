using JoggingTrackerWebApi.Dto;
using JoggingTrackerWebApi.Models;

namespace JoggingTrackerWebApi.Service
{
    public interface IJoggingService
    {
        Task<List<JoggingEntry>> GetAllAsync(string userId, bool IsAdmin );
        Task<JoggingEntry> GetByIdAsync(int id, string userId, bool IsAdmin);
        Task AddAsync(JoggingEntry entry, string userId);
        Task UpdateAsync(JoggingEntry entry, string userId);
        Task DeleteAsync(int id, string userId);
        Task<List<JoggingEntry>> FilterAsync(DateTime startDate, DateTime endDate, string userId, bool isAdmin);
        Task<JoggingReportDto> GetReportAsync(string userId, bool isAdmin);
    }
}
