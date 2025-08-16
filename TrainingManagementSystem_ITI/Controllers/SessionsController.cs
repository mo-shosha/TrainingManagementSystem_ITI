using Microsoft.AspNetCore.Mvc;
using TrainingManagementSystem_ITI.Interfaces.IRepository;
using TrainingManagementSystem_ITI.Models;
using TrainingManagementSystem_ITI.Repository;
using TrainingManagementSystem_ITI.ViewModel;

namespace TrainingManagementSystem_ITI.Controllers
{
    public class SessionsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SessionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(string searchTerm)
        {
            IEnumerable<Session> sessions;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                sessions = await _unitOfWork.SessionRepository.GetAllDetailsAsync();
            }
            else
            {
                sessions = await _unitOfWork.SessionRepository.SearchByCourseNameAsync(searchTerm);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(sessions);
        }
        public async Task<IActionResult> Create()
        {
            var viewModel = new SessionViewModel
            {
                AvailableCourses = await _unitOfWork.CourseRepository.GetAllAsync()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionViewModel viewModel)
        {
            if (viewModel.StartDate <= DateTime.Now)
            {
                ModelState.AddModelError("StartDate", "Start date cannot be in the past");
            }

            if (viewModel.EndDate <= viewModel.StartDate)
            {
                ModelState.AddModelError("EndDate", "End date must be after start date");
            }

            if (ModelState.IsValid)
            {
                var session = new Session
                {
                    CourseId = viewModel.CourseId,
                    StartDate = viewModel.StartDate,
                    EndDate = viewModel.EndDate
                };

                await _unitOfWork.SessionRepository.AddAsync(session);
                await _unitOfWork.SaveChangesAsync();

                TempData["Success"] = "Session created successfully!";
                return RedirectToAction(nameof(Index));
            }

            viewModel.AvailableCourses = await _unitOfWork.CourseRepository.GetAllAsync();
            return View(viewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var session = await _unitOfWork.SessionRepository.GetByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            var viewModel = new SessionViewModel
            {
                Id = session.Id,
                CourseId = session.CourseId,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                AvailableCourses = await _unitOfWork.CourseRepository.GetAllAsync()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SessionViewModel SessionFromRequest)
        {
            if (SessionFromRequest.StartDate <= DateTime.Now)
            {
                ModelState.AddModelError("StartDate", "Start date cannot be in the past");
            }
            if (SessionFromRequest.EndDate <= SessionFromRequest.StartDate)
            {
                ModelState.AddModelError("EndDate", "End date must be after start date");
            }
            if (ModelState.IsValid)
            {
                var session = await _unitOfWork.SessionRepository.GetByIdAsync(SessionFromRequest.Id);
                if (session == null)
                {
                    return NotFound();
                }
                session.CourseId = SessionFromRequest.CourseId;
                session.StartDate = SessionFromRequest.StartDate;
                session.EndDate = SessionFromRequest.EndDate;
                await _unitOfWork.SessionRepository.UpdateAsync(session);
                await _unitOfWork.SaveChangesAsync();
                TempData["Success"] = "Session updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            SessionFromRequest.AvailableCourses = await _unitOfWork.CourseRepository.GetAllAsync();
            return View(SessionFromRequest);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.SessionRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            TempData["Success"] = "Session deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
