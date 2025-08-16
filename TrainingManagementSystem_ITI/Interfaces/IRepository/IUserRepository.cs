using TrainingManagementSystem_ITI.Models;

namespace TrainingManagementSystem_ITI.Interfaces.IRepository
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetInstructorsAsync();
        Task<IEnumerable<User>> GetTraineesAsync();
    }
}
