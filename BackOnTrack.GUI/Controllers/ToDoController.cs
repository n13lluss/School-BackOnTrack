using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using BackOnTrack.Core.Services;
using BackOnTrack.GUI.Models;
using BackOnTrackGUI.Models.ToDo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace BackOnTrackGUI.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDOService _todoService;
        private readonly ILogger<ToDoController> _logger;

        public ToDoController(IToDOService toDOService, ILogger<ToDoController> logger)
        {
            _todoService = toDOService;
            _logger = logger;
        }

        // GET: ToDoController
        public ActionResult Index()
        {
            List<ToDo> todos = _todoService.GetAllToDos("4002");
            List<ToDoIndexViewModel> models = todos.Select(todo => new ToDoIndexViewModel
            {
                Id = todo.Id,
                Name = todo.Name,
                Description = todo.Description,
                Planned = todo.PlannedDate,
                Status = todo.GetStatusAsString(),
            }).ToList();

            return View(models.OrderByDescending(model => model.Planned));
        }



        // GET: ToDoController/Details/5
        public ActionResult Details(int id)
        {
            ToDo model = _todoService.GetToDoById(id);
            if (model == null)
            {
                return NotFound();
            }

            ToDoDetailsViewModel detail = new()
            {
                Id = model.Id,
                Name= model.Name,
                Description = model.Description,
                Planned = model.PlannedDate,
                Status = model.GetStatusAsString(),
            };
            return View(detail);
        }

        // GET: ToDoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToDoCreationViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string userId = "4002"; // Replace with the actual user ID

                ToDo existingResult = _todoService.GetToDoByNameOnDate(model.Name, userId, model.Planned);

                if (existingResult.Id != 0)
                {
                    TempData["Notification"] = "A result with this name already exists.";
                    return RedirectToAction("Details", new { id = existingResult.Id });
                }

                var todo = new ToDo
                {
                    Name = model.Name,
                    Description = model.Description,
                    PlannedDate = model.Planned,
                    Status = model.Status,
                    UserId = userId
                };

                bool isCreated = _todoService.CreateToDo(todo);

                if (isCreated)
                {
                    TempData["SuccessfullCreationNotification"] = "Result has been created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["FailedCreation"] = "Failed to add the result";
                    return RedirectToAction(nameof(Create));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a sleep result.");
                return View(model);
            }
        }

        // GET: ToDoController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = _todoService.GetToDoById(id);
            if (result == null)
            {

                return NotFound();
            }

            var edit = new ToDoEditViewModel
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Planned = result.PlannedDate,
            };
            return View(edit);
        }

        // POST: ToDoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ToDoEditViewModel changes)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(changes);
                }

                ToDo change = new()
                {
                    Id = changes.Id,
                    Name = changes.Name,
                    Description = changes.Description,
                    PlannedDate = changes.Planned,
                    UserId = "4002"
                };

                bool isEdited = _todoService.UpdateToDo(change);

                if (isEdited)
                {
                    TempData["SuccessEditNotification"] = "Changes Successfully Saved";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["EditErrorNotification"] = "Failed to save changes"; // Add an error message
                    return View(changes);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing a sleep result.");
                return View(changes);
            }
        }

        // GET: ToDoController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = _todoService.GetToDoById(id);
            if (result == null) { 
            
                return NotFound();
            }

            var delete = new ToDoDeletionViewModel
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Planned = result.PlannedDate,
                Status = result.GetStatusAsString(),
            };
            return View(delete);
        }

        // POST: ToDoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var deleted = _todoService.GetToDoById(id);
                if (deleted == null)
                {
                    return NotFound();
                }
                _todoService.DeleteToDo(deleted);
                TempData["SuccesDeletion"] = "Result has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a sleep result.");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(int id)
        {
            if(id <= 0)
            {
                return RedirectToAction(nameof(Index));
            }
            
            _todoService.UpdateStatus(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
