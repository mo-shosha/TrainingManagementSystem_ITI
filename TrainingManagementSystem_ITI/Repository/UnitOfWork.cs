using TrainingManagementSystem_ITI.Data;
using TrainingManagementSystem_ITI.Interfaces.IRepository;

namespace TrainingManagementSystem_ITI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ICourseRepository _courseRepository;
        private IUserRepository _userRepository;
        private ISessionRepository _sessionRepository;
        private IGradeRepository _gradeRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public ICourseRepository CourseRepository => _courseRepository ??= new CourseRepository(_context);
        public ISessionRepository SessionRepository => _sessionRepository ??= new SessionRepository(_context);
        public IGradeRepository GradeRepository => _gradeRepository ??= new GradeRepository(_context);


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
