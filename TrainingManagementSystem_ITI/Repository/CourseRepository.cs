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
    }
    
}
