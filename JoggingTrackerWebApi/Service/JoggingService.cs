using JoggingTrackerWebApi.Models;
using JoggingTrackerWebApi.Dto;

using JoggingTrackerWebApi.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JoggingTrackerWebApi.Service
{
    public class JoggingService: IJoggingService
    {
        private readonly IJoggingRepository _repo;
        public JoggingService(IJoggingRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<JoggingEntry>> GetAllAsync(string userId, bool IsAdmin)
        {
            if (IsAdmin)
            {
                return await _repo.GetAllAsync();
            }
            return await _repo.GetAllByUserAsync(userId);
        }
        public async Task<JoggingEntry> GetByIdAsync(int Id, string userId, bool IsAdmin)
        {
            if (IsAdmin)
            {
                return await _repo.GetByIdAsync(Id);
            }
            return await _repo.GetByIdByUserAsync(Id, userId);
        }
        public async Task AddAsync(JoggingEntry entry, string userId)
        {
            entry.UserId = userId;
            await _repo.AddAsync(entry);
        }
        public async Task UpdateAsync(JoggingEntry entry, string userId)
        {
            var existing = await _repo.GetByIdAsync(entry.Id, userId);
            if (existing == null) throw new Exception("Not found");

            existing.Date = entry.Date;
            existing.Distance = entry.Distance;
            existing.Duration = entry.Duration;

            await _repo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id, string userId)
        {
            
            var existing = await _repo.GetByIdByUserAsync(id, userId);
            if (existing == null) throw new Exception("Not found");

            await _repo.DeleteAsync(existing);
        }
        public async Task<List<JoggingEntry>> FilterAsync(DateTime startDate, DateTime endDate, string userId, bool isAdmin)
        {
            if (isAdmin)
                return await _repo.FilterAllAsync(startDate, endDate);

            return await _repo.FilterByUserAsync(startDate, endDate, userId);
        }

        public async Task<JoggingReportDto> GetReportAsync(string userId, bool IsAdmin)
        {
            List<JoggingEntry> entries;
            if (IsAdmin)
                entries= await _repo.GetAllAsync();
            else
                entries= await _repo.GetAllByUserAsync(userId);

            var report = new JoggingReportDto
            {
                TotalDistance = entries.Sum(x => x.Distance),
                AverageDistance = entries.Count > 0 ? entries.Average(x => x.Distance) : 0,
                AverageDuration = entries.Count > 0 ? entries.Average(x => x.Duration) : 0
            };
            return report;

        }

    }
}
