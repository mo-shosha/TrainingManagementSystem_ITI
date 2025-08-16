using System.ComponentModel.DataAnnotations;
using TrainingManagementSystem_ITI.Models;

namespace TrainingManagementSystem_ITI.ViewModel
{
    public class SessionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course is required")]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public IEnumerable<Course> AvailableCourses { get; set; } = new List<Course>();
    }
}
