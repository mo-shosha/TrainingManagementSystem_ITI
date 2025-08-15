using System.ComponentModel.DataAnnotations;

namespace TrainingManagementSystem_ITI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; } 

        public ICollection<Course> Courses { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }

}
