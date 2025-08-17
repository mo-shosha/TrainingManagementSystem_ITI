using Microsoft.EntityFrameworkCore;
using TrainingManagementSystem_ITI.Data;
using TrainingManagementSystem_ITI.Interfaces.IRepository;
using TrainingManagementSystem_ITI.Models;

namespace TrainingManagementSystem_ITI.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<IEnumerable<User>> GetInstructorsAsync()
        {
            return await _db.Users.Where(u => u.Role == "Instructor").ToListAsync();
        }

        public async Task<IEnumerable<User>> GetTraineesAsync()
        {
            return await _db.Users.Where(u => u.Role == "Trainee").ToListAsync();
        }
    }
}
