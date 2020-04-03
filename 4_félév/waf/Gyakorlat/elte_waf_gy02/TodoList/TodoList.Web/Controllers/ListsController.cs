using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoList.Web.Models;
using TodoList.Web.Services;

namespace TodoList.Web.Controllers
{
    public class ListsController : Controller
    {
        private readonly TodoListService _service;

        public ListsController(TodoListService service)
        {
            _service = service;
        }

        // GET: Lists
        public IActionResult Index()
        {
            return View(_service.GetLists());
        }

        // GET: Lists/Details/5
        public IActionResult Details(int id, string sortOrder = "")
        {
            var list = _service.GetListById(id);
            if (list == null)
            {
                return NotFound();
            }

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
    }
}
