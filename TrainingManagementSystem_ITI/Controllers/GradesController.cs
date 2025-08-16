using Microsoft.AspNetCore.Mvc;
using TrainingManagementSystem_ITI.Interfaces.IRepository;
using TrainingManagementSystem_ITI.Models;
using TrainingManagementSystem_ITI.ViewModel;

namespace TrainingManagementSystem_ITI.Controllers
{
    public class GradesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GradesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Grades
        public async Task<IActionResult> Index()
        {
            var grades = await _unitOfWork.GradeRepository.GetAllWithDetailsAsync();

            var vm = grades.Select(g => new GradeViewModel
            {
                Id = g.Id,
                TraineeName = g.Trainee.Name,
                CourseName = g.Session.Course.Name,
                Value = g.Value
            }).ToList();

            return View(vm);
        }

        // GET: Grades/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Sessions = await _unitOfWork.SessionRepository.GetAllAsync(s => s.Course);
            ViewBag.Trainees = (await _unitOfWork.UserRepository.GetAllAsync())
                .Where(u => u.Role == "Trainee");
            return View();

        }

        // POST: Grades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Grade grade)
        {

            if (ModelState.IsValid)
            {
                await _unitOfWork.GradeRepository.AddAsync(new Grade
                {
                    SessionId = grade.SessionId,
                    TraineeId = grade.TraineeId,
                    Value = grade.Value
                });
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Sessions = await _unitOfWork.SessionRepository.GetAllAsync(s => s.Course);
            ViewBag.Trainees = await _unitOfWork.UserRepository.GetAllAsync();
            return View(grade);
        }

        // GET: Grades/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var grade = await _unitOfWork.GradeRepository.GetByIdAsync(id);
            if (grade == null) return NotFound();

            ViewBag.Sessions = await _unitOfWork.SessionRepository.GetAllAsync(s => s.Course);
            ViewBag.Trainees = await _unitOfWork.UserRepository.GetAllAsync();
            return View(grade);
        }

        // POST: Grades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Grade grade)
        {
            if (id != grade.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _unitOfWork.GradeRepository.UpdateAsync(grade);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // GET: Grades/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _unitOfWork.GradeRepository.GetByIdAsync(id, g => g.Trainee, g => g.Session.Course);
            if (grade == null) return NotFound();

            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _unitOfWork.GradeRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
