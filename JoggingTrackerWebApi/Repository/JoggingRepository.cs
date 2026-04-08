using JoggingTrackerWebApi.Data;
using JoggingTrackerWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JoggingTrackerWebApi.Repository
{
    public class JoggingRepository : IJoggingRepository
    {
        private readonly AppDbContext _context;

        public JoggingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<JoggingEntry>> GetAllAsync()
        {
            return await _context.JoggingEntries.ToListAsync();
        }
        public async Task<List<JoggingEntry>> GetAllByUserAsync(string userId)
        {
            return await _context.JoggingEntries
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<JoggingEntry?> GetByIdAsync(int id)
        {
            return await _context.JoggingEntries
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<JoggingEntry?> GetByIdByUserAsync(int id, string userId)
        {
            return await _context.JoggingEntries
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }
        public async Task AddAsync(JoggingEntry entry)
        {
            await _context.JoggingEntries.AddAsync(entry);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(JoggingEntry entry)
        {
            _context.JoggingEntries.Update(entry);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(JoggingEntry entry)
        {
            _context.JoggingEntries.Remove(entry);
            await _context.SaveChangesAsync();
        }
        public async Task<List<JoggingEntry>> FilterAllAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.JoggingEntries
                .Where(x => x.Date >= startDate && x.Date <= endDate)
                .ToListAsync();
        }

        public async Task<List<JoggingEntry>> FilterByUserAsync(DateTime startDate, DateTime endDate, string userId)
        {
            return await _context.JoggingEntries
                .Where(x => x.UserId == userId &&
                            x.Date >= startDate &&
                            x.Date <= endDate)
                .ToListAsync();
        }
    }
}
