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
    }
    
}
