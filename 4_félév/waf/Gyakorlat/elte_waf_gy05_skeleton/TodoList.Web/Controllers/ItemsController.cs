using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using TodoList.Persistence;
using TodoList.Persistence.Services;

namespace TodoList.Web.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly TodoListService _service;

        public ItemsController(TodoListService service)
        {
            _service = service;
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["Lists"] = new SelectList(_service.GetLists(), "Id", "Name", TempData["ListId"]);
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Description,Deadline,ListId")] Item item, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    image.CopyTo(stream);
                    item.Image = stream.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
                _service.CreateItem(item);
                return RedirectToAction("Details", "Lists", new { id = item.ListId });
            }

            ViewData["Lists"] = new SelectList(_service.GetLists(), "Id", "Name", item.ListId);
            return View(item);
        }

        // GET: Items/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _service.GetItem((int)id);
            if (item == null)
            {
                return NotFound();
            }

            ViewData["Lists"] = new SelectList(_service.GetLists(), "Id", "Name", item.ListId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,Deadline,ListId")] Item item, IFormFile image)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (image != null && image.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    image.CopyTo(stream);
                    item.Image = stream.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
                bool result;
                if (item.Image != null)
                {
                    result = _service.UpdateItem(item);
                }
                else
                {
                    result = _service.UpdateItemExcludeImage(item);
                }
                
                if (result)
                {
                    return RedirectToAction("Details", "Lists", new { id = item.ListId });
                }
                else
                {
                    ModelState.AddModelError("", "Hiba történt a mentés során!");
                }
            }

            ViewData["Lists"] = new SelectList(_service.GetLists(), "Id", "Name", item.ListId);
            return View(item);
        }

        // GET: Items/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _service.GetItem((int)id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _service.GetItem((int)id);
            if (item != null)
            {
                _service.DeleteItem(id);
                return RedirectToAction("Details", "Lists", new { id = item.ListId });
            }
            
            return NotFound();
        }

        [AllowAnonymous]
        public IActionResult DisplayImage(int id)
        {
            var item = _service.GetItem(id);
            return File(item.Image, "image/png");
        }
    }
}
