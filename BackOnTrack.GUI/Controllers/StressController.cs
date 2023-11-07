using Microsoft.AspNetCore.Mvc;

namespace BackOnTrack.GUI.Controllers
{
    public class StressController : Controller
    {
        // GET: StressController
        public ActionResult Index()
        {
            return View();
        }

        // GET: StressController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
