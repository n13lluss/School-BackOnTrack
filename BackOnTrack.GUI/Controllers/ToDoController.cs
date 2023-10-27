using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using BackOnTrack.GUI.Models;
using BackOnTrackGUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnTrackGUI.Controllers
{
    public class ToDoController : Controller
    {

        private readonly IToDOService _ToDoService;

        public ToDoController(IToDOService toDoService)
        {
            _ToDoService = toDoService;
        }
        // GET: ToDoController
        public ActionResult Index()
        {
            List<ToDoViewModel> toDos = new List<ToDoViewModel>();
            //List<ToDo> ToDoModels = _ToDoService.GetAllToDos();
            //foreach (ToDo toDo in ToDoModels)
            //{
            //    ToDoViewModel toDoViewModel = new()
            //    {
            //        Title =  toDo.Name,
            //        Description = toDo.Description,
            //        Planned = toDo.PlannedDate
            //    };
            //    toDos.Add(toDoViewModel);
            //}
            ToDoViewModel toDo = new()
            {
                Title = "This is a test run",
                Description = "this is a test file where im testing if the ui works correctly",
                Planned = DateTime.Now,
                Status = 1
            };

            toDos.Add(toDo);

            return View(toDos);
        }

        // GET: ToDoController/Details/5
        public ActionResult Details(int id)
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
        public ActionResult Edit(int id)
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
        public ActionResult Delete(int id)
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
