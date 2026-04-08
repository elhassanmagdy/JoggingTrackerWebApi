using JoggingTrackerWebApi.Models;

namespace JoggingTrackerWebApi.Repository
{
    public interface IJoggingRepository
    {
          //GetAll
        Task<List<JoggingEntry>> GetAllAsync();
        Task<List<JoggingEntry>> GetAllByUserAsync(string userId);

          //GetById
        Task<JoggingEntry> GetByIdAsync(int id);
        Task<JoggingEntry> GetByIdByUserAsync(int id, string userId);
          //Add
        Task AddAsync(JoggingEntry entry);
          //Update
        Task UpdateAsync(JoggingEntry entry);
          //Delete
        Task DeleteAsync(JoggingEntry entry);
        //Filter
        Task<List<JoggingEntry>> FilterAllAsync(DateTime startDate, DateTime endDate);
        Task<List<JoggingEntry>> FilterByUserAsync(DateTime startDate, DateTime endDate, string userId);
    }
}
