namespace TrainingManagementSystem_ITI.Interfaces.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository UserRepository { get; }
        ICourseRepository CourseRepository { get; }
        ISessionRepository SessionRepository { get; }
        IGradeRepository GradeRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
