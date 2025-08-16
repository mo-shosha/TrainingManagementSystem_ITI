using TrainingManagementSystem_ITI.Models;

namespace TrainingManagementSystem_ITI.Interfaces.IRepository
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllWithDetailsAsync();
        Task<IEnumerable<Course>> SearchAsync(string searchTerm);
        Task<bool> IsCourseNameUniqueAsync(string name, int? excludeId = null);
    }
}
