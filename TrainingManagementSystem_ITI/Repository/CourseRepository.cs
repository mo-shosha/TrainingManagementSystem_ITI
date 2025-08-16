using Microsoft.EntityFrameworkCore;
using TrainingManagementSystem_ITI.Data;
using TrainingManagementSystem_ITI.Interfaces.IRepository;
using TrainingManagementSystem_ITI.Models;

namespace TrainingManagementSystem_ITI.Repository
{
    public class CourseRepository:BaseRepository<Course>, ICourseRepository
    {
        private readonly AppDbContext _db;
        public CourseRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Course>> GetAllWithDetailsAsync()
        {
            return await _db.Courses
                .Include(c => c.Instructor)
                .ToListAsync();
        }
        public async Task<IEnumerable<Course>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            return await _db.Courses.Include(c => c.Instructor)
                .Where(c => c.Name.Contains(searchTerm) || c.Category.Contains(searchTerm))
                .ToListAsync();
        }
        public async Task<bool> IsCourseNameUniqueAsync(string name, int? excludeId = null)
        {
            var query = _db.Courses.Where(c => c.Name == name);
            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);

            return !await query.AnyAsync();
        }
    }
    
}
