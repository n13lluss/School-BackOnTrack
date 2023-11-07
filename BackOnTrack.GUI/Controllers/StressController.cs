using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using BackOnTrack.GUI.Models.SleepResult;
using BackOnTrack.GUI.Models.Stress;
using Microsoft.AspNetCore.Mvc;

namespace BackOnTrack.GUI.Controllers
{
    public class StressController : Controller
    {
        private readonly IStressService _stressService;

        public StressController(IStressService stressService)
        {
            _stressService = stressService;
        }

        public ActionResult Index()
        {
            List<StressResult> results = _stressService.GetAllStressResults("4002"); // Replace "userId" with the actual user identifier.
            Console.WriteLine("test");
            List<StressResultViewModel> viewmodels = results.Select(r => new StressResultViewModel()
            {
                Id = r.Id,
                Result = r.StressLevel,
                Date = r.date,
                HoursSlept = r.HoursSlept
            }).ToList();

            return View(viewmodels);
        }

        // GET: StressController/Details/5
        public ActionResult Details(int id)
        { 
            DetailsStressViewModel model = new DetailsStressViewModel();
            return View(model);
        }

        // GET: StressController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StressController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StressController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StressController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StressController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StressController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
