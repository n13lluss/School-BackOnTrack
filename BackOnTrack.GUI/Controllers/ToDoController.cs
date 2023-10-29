using BackOnTrackGUI.Models.ToDo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnTrackGUI.Controllers
{
    public class ToDoController : Controller
    {
        // GET: ToDoController
        public ActionResult Index()
        {
            List<ToDoIndexViewModel> models = new();
            ToDoIndexViewModel model = new()
            {
                Id = 1,
                Name = "Test 1",
                Description = "This is a test if the description tab expandes is the test gets larger",
                Planned = DateTime.Today,
                Status = "Not Yet Started"
            };
            models.Add(model); 
            return View(models);
        }

        // GET: ToDoController/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: ToDoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoController/Create
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

        // GET: ToDoController/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: ToDoController/Edit/5
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

        // GET: ToDoController/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: ToDoController/Delete/5
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
