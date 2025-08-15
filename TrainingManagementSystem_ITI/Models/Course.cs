using System.ComponentModel.DataAnnotations;

namespace TrainingManagementSystem_ITI.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int InstructorId { get; set; }
        public User Instructor { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }

}
