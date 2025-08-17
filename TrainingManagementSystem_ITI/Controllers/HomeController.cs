using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrainingManagementSystem_ITI.Models;
using TrainingManagementSystem_ITI.Data;
using System.Linq;
using TrainingManagementSystem_ITI.Interfaces.IRepository;
using TrainingManagementSystem_ITI.ViewModel;

namespace TrainingManagementSystem_ITI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> IndexAsync()
        {

            var totalStudents = (await _unitOfWork.UserRepository.GetAllAsync())
                                .Where(u => u.Role == "Trainee")
                                .Count();


            var totalCourses = await _unitOfWork.CourseRepository.CountAsync();


            var startOfWeek = DateHelper.StartOfWeek(DateTime.Now, DayOfWeek.Saturday);
            var endOfWeek = startOfWeek.AddDays(7);

            var sessionsThisWeek = (await _unitOfWork.SessionRepository.GetAllAsync())
                                    .Where(s => s.StartDate >= startOfWeek && s.EndDate < endOfWeek)
                                    .Count();

            var grades = await _unitOfWork.GradeRepository.GetAllAsync();
            var averageGrade = grades.Any() ? grades.Average(g => g.Value) : 0;

            var dashboardVM = new DashboardViewModel
            {
                TotalStudents = totalStudents,
                TotalCourses = totalCourses,
                SessionsThisWeek = sessionsThisWeek,
                AverageGrade = averageGrade
            };

            return View(dashboardVM);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        private class DateHelper
        {
            public static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
            {
                int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
                return dt.AddDays(-1 * diff).Date;
            }
        }
    }
}