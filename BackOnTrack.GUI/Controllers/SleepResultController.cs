using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using BackOnTrack.GUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.VisualBasic.FileIO;
using System.Threading.Channels;

namespace BackOnTrack.GUI.Controllers
{
    public class SleepResultController : Controller
    {
        private readonly ISleepService _sleepService;

        public SleepResultController(ISleepService sleepService)
        {
            _sleepService = sleepService;
        }

        // GET: SleepResultController
        public ActionResult Index()
        {
            List<SleepResultViewModel> sleepResults = new List<SleepResultViewModel>();
            List<SleepResult> models = _sleepService.GetResultList();

            foreach(SleepResult model in models)
            {
                SleepResultViewModel sleepresult = new()
                {
                    Id = model.Id,
                    TimeSlept = model.HoursSlept,
                    Date = Convert.ToDateTime(model.Date.ToString()),
                };
                sleepResults.Add(sleepresult);
            }

            return View(sleepResults);
        }

        // GET: SleepResultController/Details/5
        public ActionResult Details(int id)
        {
            SleepResult model = _sleepService.GetById(id);
            SleepResultViewModel detail = new()
            {
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string? userId = "4002";

                SleepResult result = new()
                {
                    HoursSlept = int.Parse(collection["TimeSlept"]),
                    Date = DateTime.Parse(collection["Date"]),
                    UserID = userId
                };

                SleepResult CreatedResult = _sleepService.CreateResult(result);

                SleepResultViewModel created = new()
                {
                    TimeSlept = CreatedResult.HoursSlept,
                    Date = CreatedResult.Date,
                };

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SleepResultController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SleepResultController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SleepResultViewModel changes)
        {
            try
            {
                SleepResult change = new()
                {
                    Id = changes.Id,
                    HoursSlept = changes.TimeSlept,
                    Date = changes.Date,
                    UserID = "4002",
                };
                _sleepService.EditResult(change);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SleepResultController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SleepResultController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string user)
        {

            try
            {
                SleepResult deleted = _sleepService.GetById(id);
                _sleepService.DeleteResult(deleted);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
