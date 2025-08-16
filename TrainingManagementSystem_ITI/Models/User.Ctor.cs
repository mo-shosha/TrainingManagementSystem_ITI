using TrainingManagementSystem_ITI.View_Models.Users;

namespace TrainingManagementSystem_ITI.Models
{
    public partial class User
    {
        public User(UserViewModel UserVM)
        {
            this.Role = UserVM.Role;
            this.Email = UserVM.Email;
            this.Name = UserVM.Name;
            this.Id = UserVM.Id;
        }
        public User()
        {

        }
    }
}
