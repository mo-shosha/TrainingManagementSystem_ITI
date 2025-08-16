using Microsoft.AspNetCore.Mvc;
using TrainingManagementSystem_ITI.Interfaces.IRepository;
using TrainingManagementSystem_ITI.Models;
using TrainingManagementSystem_ITI.View_Models.Users;

namespace TrainingManagementSystem_ITI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUnitOfWork UnitOfWork;
        public UsersController(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                IReadOnlyList<User> AllUsers = await UnitOfWork.UserRepository.GetAllAsync();
                List<UserViewModel> UsersViewModels = AllUsers.Select(U => new UserViewModel(U)).ToList();
                return View(UsersViewModels);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Create(UserViewModel UserFromClient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User NewUser = new User(UserFromClient);
                    UnitOfWork.UserRepository.AddAsync(NewUser);
                    return RedirectToAction(nameof(Index));
                }
                return View(UserFromClient);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            try
            {
                User UserThatClientWantToEdit = await UnitOfWork.UserRepository.GetByIdAsync(Id);
                if (UserThatClientWantToEdit is null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(UserThatClientWantToEdit);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Edit(User UserFromClientThatUpdated)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(UserFromClientThatUpdated);
                }
                UnitOfWork.UserRepository.UpdateAsync(UserFromClientThatUpdated);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                User UserThatClientWantToEdit = await UnitOfWork.UserRepository.GetByIdAsync(Id);
                if (UserThatClientWantToEdit is null)
                {
                    return RedirectToAction(nameof(Index));
                }
                await UnitOfWork.UserRepository.DeleteAsync(Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

    }
}
