using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TrainingManagementSystem_ITI.Models;

namespace TrainingManagementSystem_ITI.ViewModel
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Display(Name = "Instructor")]
        public int InstructorId { get; set; }

        //public string InstructorName { get; set; }
        public IEnumerable<User> AvailableInstructors { get; set; } = new List<User>();
    }
}
