using TrainingManagementSystem_ITI.Models;

namespace TrainingManagementSystem_ITI.View_Models.Users
{
    public partial class UserViewModel
    {
        public UserViewModel(User User)
        {
            this.Role = User.Role;
            this.Email = User.Email;
            this.Name = User.Name;
            this.Id = User.Id;
        }
        public UserViewModel()
        {

        }
    }
}
