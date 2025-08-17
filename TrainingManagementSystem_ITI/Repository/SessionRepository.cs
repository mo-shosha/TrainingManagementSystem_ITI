using Microsoft.EntityFrameworkCore;
using TrainingManagementSystem_ITI.Data;
using TrainingManagementSystem_ITI.Interfaces.IRepository;
using TrainingManagementSystem_ITI.Models;

namespace TrainingManagementSystem_ITI.Repository
{
    public class SessionRepository:BaseRepository<Session>, ISessionRepository
    {
        private readonly AppDbContext _db;
        public SessionRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Session>> SearchByCourseNameAsync(string courseName)
        {
            if (string.IsNullOrWhiteSpace(courseName))
                return await GetAllAsync();

            return await _db.Sessions.Include(s => s.Course)
                .Where(s => s.Course.Name.Contains(courseName))
                .ToListAsync();
        }
        public async Task<IEnumerable<Session>> GetAllDetailsAsync()
        {
            return await _db.Sessions
                .Include(s => s.Course)
                .ToListAsync();
        }
    }
    
}
