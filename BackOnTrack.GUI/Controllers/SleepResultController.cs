using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using BackOnTrack.GUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackOnTrack.GUI.Controllers
{
    public class SleepResultController : Controller
    {
        private readonly ISleepService _sleepService;
        private readonly ILogger<SleepResultController> _logger;

        public SleepResultController(ISleepService sleepService, ILogger<SleepResultController> logger)
        {
            _sleepService = sleepService;
            _logger = logger;
        }

        // GET: SleepResultController
        public ActionResult Index()
        {
            List<SleepResult> models = _sleepService.GetResultList();
            List<SleepResultViewModel> sleepResults = models.Select(model => new SleepResultViewModel
            {
                Id = model.Id,
                TimeSlept = model.HoursSlept,
                Date = Convert.ToDateTime(model.Date.ToString()),
            }).ToList();

            SleepResultViewModelIndex indexData = new()
            {
                AverageTimeSlept = _sleepService.GetAverageTimeSleptLastSevenDays("4002"),
                AllResults = sleepResults
            };

            return View(indexData);
        }

        // GET: SleepResultController/Details/5
        public ActionResult Details(int id)
        {
            SleepResult model = _sleepService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            SleepResultViewModel detail = new()
            {
                Id = model.Id,
                Date = model.Date,
                TimeSlept = model.HoursSlept
            };
            return View(detail);
        }

        // GET: SleepResultController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SleepResultController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SleepResultViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                string userId = "4002"; // Replace with the actual user ID

                var existingResult = _sleepService.GetResultByDateAndUserId(viewModel.Date, userId);

                if (existingResult.Id != 0)
                {
                    TempData["Notification"] = "A result for this date already exists.";
                    return RedirectToAction("Details", new { id = existingResult.Id });
                }

                var result = new SleepResult
                {
                    HoursSlept = viewModel.TimeSlept,
                    Date = viewModel.Date,
                    UserID = userId
                };

                bool isCreated = _sleepService.CreateResult(result);

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
    


        // GET: SleepResultController/Edit/5
        public ActionResult Edit(int id)
        {
            SleepResult result = _sleepService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            SleepResultViewModel edit = new()
            {
                Id = result.Id,
                TimeSlept = result.HoursSlept,
                Date = result.Date,
            };

            return View(edit);
        }

        // POST: SleepResultController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SleepResultViewModel changes)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(changes);
                }

                SleepResult change = new()
                {
                    Id = changes.Id,
                    HoursSlept = changes.TimeSlept,
                    Date = changes.Date,
                    UserID = "4002",
                };

                bool isEdited = _sleepService.EditResult(change);

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
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing a sleep result.");
                return View(changes);
            }
        }

        // GET: SleepResultController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = _sleepService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            var delete = new SleepResultViewModel
            {
                Id = result.Id,
                TimeSlept = result.HoursSlept,
                Date = result.Date,
            };
            return View(delete);
        }

        // POST: SleepResultController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var deleted = _sleepService.GetById(id);
                if (deleted == null)
                {
                    return NotFound();
                }
                _sleepService.DeleteResult(deleted);
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
