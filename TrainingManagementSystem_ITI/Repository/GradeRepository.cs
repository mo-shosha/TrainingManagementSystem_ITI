using TrainingManagementSystem_ITI.Data;
using TrainingManagementSystem_ITI.Interfaces.IRepository;
using TrainingManagementSystem_ITI.Models;

namespace TrainingManagementSystem_ITI.Repository
{
    public class GradeRepository : BaseRepository<Grade>, IGradeRepository
    {
        private readonly AppDbContext _db;
        public GradeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
   
}
