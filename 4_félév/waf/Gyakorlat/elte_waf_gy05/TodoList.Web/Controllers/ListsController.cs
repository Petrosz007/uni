using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TodoList.Persistence;
using TodoList.Persistence.Services;

namespace TodoList.Web.Controllers
{
    [Authorize]
    public class ListsController : Controller
    {
        private readonly TodoListService _service;

        public ListsController(TodoListService service)
        {
            _service = service;
        }

        // GET: Lists
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_service.GetLists());
        }

        // GET: Lists/Details/5
        [AllowAnonymous]
        public IActionResult Details(int id, string sortOrder = "")
        {

            try
            {
                var list = _service.GetListById(id);

                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewData["DeadlineSortParm"] = sortOrder == "deadline_asc" ? "deadline_desc" : "deadline_asc";

                switch (sortOrder)
                {
                    case "deadline_asc":
                        list.Items = list.Items.OrderBy(i => i.Deadline).ToList();
                        break;
                    case "deadline_desc":
                        list.Items = list.Items.OrderByDescending(i => i.Deadline).ToList();
                        break;
                    case "name_desc":
                        list.Items = list.Items.OrderByDescending(i => i.Name).ToList();
                        break;
                    default:
                        list.Items = list.Items.OrderBy(i => i.Name).ToList();
                        break;
                }

                return View(list);
            }
            catch (InvalidOperationException) // when GetListById fails
            {
                return NotFound();
            }
        }

        // GET: Lists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] List list)
        {
            if (ModelState.IsValid)
            {
                _service.CreateList(list);
                return RedirectToAction(nameof(Index));
            }
            return View(list);
        }

        // GET: Lists/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var list = _service.GetListById((int)id);
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }

        // POST: Lists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] List list)
        {
            if (id != list.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool result = _service.UpdateList(list);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Hiba történt a mentés során!");
                }
            }

            return View(list);
        }

        // GET: Lists/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var list = _service.GetListById((int)id);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);
        }

        // POST: Lists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.DeleteList(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateItem(int id)
        {
            TempData["ListId"] = id;
            return RedirectToAction("Create", "Items");
        }
    }
}
