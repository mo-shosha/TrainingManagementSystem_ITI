using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrainingManagementSystem_ITI.Models;
using TrainingManagementSystem_ITI.Data;
using System.Linq;

namespace TrainingManagementSystem_ITI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context; 

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult TestDb()
        {
            var result = new
            {
                Courses = _context.Courses
                                  .Select(c => new { c.Id, c.Name, c.Category, Instructor = c.Instructor.Name })
                                  .ToList(),
                Grades = _context.Grades
                                 .Select(g => new { g.Id, g.Value, SessionId = g.SessionId, Trainee = g.Trainee.Name })
                                 .ToList()
            };

            return Json(result);
        }
    }
}