using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using BackOnTrack.Core.Services;
using BackOnTrack.GUI.Models.SleepResult;
using BackOnTrack.GUI.Models.Stress;
using BackOnTrack.GUI.Models.ToDo;
using Microsoft.AspNetCore.Mvc;

namespace BackOnTrack.GUI.Controllers
{
    public class StressController : Controller
    {
        private const string TestUserId = "4002";

        private readonly IStressService _stressService;
        private readonly IToDOService _todoService;
        private readonly ILogger<StressController> _logger;

        public StressController(IStressService stressService, IToDOService toDOService, ILogger<StressController> logger)
        {
            _stressService = stressService;
            _todoService = toDOService;
            _logger = logger;
        }

        public ActionResult Index()
        {
            List<StressResult> results = _stressService.GetAllStressResults(TestUserId); // 4002 is user id
            List<StressResultViewModel> viewmodels = results.Select(r => new StressResultViewModel()
            {
                Id = r.Id,
                Result = r.GetStressAsString(),
                Date = r.date,
                HoursSlept = r.HoursSlept
            }).ToList();

            return View(viewmodels);
        }

        // GET: StressController/Details/5
        public ActionResult Details(int id)
        {
            string userId = TestUserId;
            StressResult model = _stressService.GetStressResultById(id);
            DetailsStressViewModel viewModel = new()
            {
                Id = model.Id,
                date = model.date,
                Result = model.GetStressAsString(),
                SleptLastNight = model.HoursSlept,
                TasksPlanned = _todoService.GetToDoByDate(model.date, userId).Count(),
                ToDos = _todoService.GetToDoByDate(model.date, userId).Select(todo => new ToDoIndexViewModel()
                {
                    Name = todo.Name,
                    Description = todo.Description
                }).ToList()
            };
            return View(viewModel);
        }

        // GET: StressController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StressController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateStressViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                string userId = "4002"; // Replace with the actual user ID

                var existingResult = _stressService.GetStressResultByDateAndId(viewModel.date, userId);

                if (existingResult.Id != 0)
                {
                    TempData["Notification"] = "A result for this date already exists.";
                    return RedirectToAction("Details", new { id = existingResult.Id });
                }

                var result = new StressResult
                {
                    StressLevel = viewModel.Result,
                    date = viewModel.date,
                    UserId = userId,
                };

                bool isCreated = _stressService.CreateStressResult(result);

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
                return View(viewModel);
            }
        }

        // GET: StressController/Edit/5
        public ActionResult Edit(int id)
        {
            StressResult result = _stressService.GetStressResultById(id);

            if (result == null)
            {
                return NotFound();
            }

            EditStressViewModel edit = new()
            {
                Id = result.Id,
                date = result.date,
                Result = result.StressLevel
            };

            return View(edit);
        }

        // POST: StressController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditStressViewModel changes)
        {
            string userId = "4002";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(changes);
                }

                StressResult change = new()
                {
                    Id = changes.Id,
                    StressLevel = changes.Result,
                    date = changes.date,
                    UserId = userId     
                };

                bool isEdited = _stressService.UpdateStressResult(change);

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

        // GET: StressController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = _stressService.GetStressResultById(id);
            if (result == null)
            {
                return NotFound();
            }

            var delete = new DeleteStressResultViewModel
            {
                Id = result.Id,
                Result = result.GetStressAsString(),
                date = result.date
            };
            return View(delete);
        }

        // POST: StressController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var deleted = _stressService.GetStressResultById(id);
                if (deleted == null)
                {
                    return NotFound();
                }
                _stressService.DeleteStressResult(deleted);
                TempData["SuccesDeletion"] = "Result has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a sleep result.");
                return View();
            }
        }
    }
}
