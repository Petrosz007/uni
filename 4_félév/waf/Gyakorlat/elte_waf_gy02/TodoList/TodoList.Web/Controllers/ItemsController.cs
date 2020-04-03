using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoList.Web.Models;
using TodoList.Web.Services;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace TodoList.Web.Controllers
{
    public class ItemsController : Controller
    {
        private readonly TodoListService _service;

        public ItemsController(TodoListService service)
        {
            _service = service;
        }

        public IActionResult DisplayImage(int id)
        {
            var item = _service.GetItem(id);
            return File(item.Image, "image/png");
        }
    }
}
