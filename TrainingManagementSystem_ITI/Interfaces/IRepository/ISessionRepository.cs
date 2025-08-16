using TrainingManagementSystem_ITI.Models;

namespace TrainingManagementSystem_ITI.Interfaces.IRepository
{
    public interface ISessionRepository : IBaseRepository<Session>
    {
        Task<IEnumerable<Session>> SearchByCourseNameAsync(string courseName);
        Task<IEnumerable<Session>> GetAllDetailsAsync();
    }
}
