using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingManagementSystem_ITI.Interfaces.IRepository;
using TrainingManagementSystem_ITI.Models;
using TrainingManagementSystem_ITI.Repository;
using TrainingManagementSystem_ITI.ViewModel;

namespace TrainingManagementSystem_ITI.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(string searchTerm)
        {
            IEnumerable<Course> courses;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                courses = await _unitOfWork.CourseRepository.GetAllWithDetailsAsync();
            }
            else
            {
                courses = await _unitOfWork.CourseRepository.SearchAsync(searchTerm);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(courses);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new CourseViewModel
            {
                AvailableInstructors = await _unitOfWork.UserRepository.GetInstructorsAsync()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel courseVM)
        {
            if (!await _unitOfWork.CourseRepository.IsCourseNameUniqueAsync(courseVM.Name))
            {
                ModelState.AddModelError("Name", "Course name must be unique");
            }

            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    Name = courseVM.Name,
                    Category = courseVM.Category,
                    InstructorId = courseVM.InstructorId
                };

                await _unitOfWork.CourseRepository.AddAsync(course);
                await _unitOfWork.SaveChangesAsync();

                TempData["Success"] = "Course created successfully!";
                return RedirectToAction(nameof(Index));
            }

            courseVM.AvailableInstructors = await _unitOfWork.UserRepository.GetInstructorsAsync();
            return View(courseVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(id);
            if (course == null) return NotFound();

            var viewModel = new CourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Category = course.Category,
                InstructorId = course.InstructorId,
                AvailableInstructors = await _unitOfWork.UserRepository.GetInstructorsAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseViewModel courseFromRequest)
        {
            if (!await _unitOfWork.CourseRepository.IsCourseNameUniqueAsync(courseFromRequest.Name, courseFromRequest.Id))
            {
                ModelState.AddModelError("Name", "Course name must be unique");
            }

            if (ModelState.IsValid)
            {
                var course = await _unitOfWork.CourseRepository.GetByIdAsync(courseFromRequest.Id);
                if (course == null) return NotFound();

                course.Name = courseFromRequest.Name;
                course.Category = courseFromRequest.Category;
                course.InstructorId = courseFromRequest.InstructorId;

                await _unitOfWork.CourseRepository.UpdateAsync(course);
                await _unitOfWork.SaveChangesAsync();

                TempData["Success"] = "Course updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            courseFromRequest.AvailableInstructors = await _unitOfWork.UserRepository.GetInstructorsAsync();
            return View(courseFromRequest);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return Json(new { success = false, message = "Course not found" });
            }

            await _unitOfWork.CourseRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
