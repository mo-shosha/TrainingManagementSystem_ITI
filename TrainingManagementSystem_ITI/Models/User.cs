using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TrainingManagementSystem_ITI.Models
{
    public partial class User
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
        [ValidateNever]
        public ICollection<Course>? Courses { get; set; }
        [ValidateNever]
        public ICollection<Grade>? Grades { get; set; }
    }

}
