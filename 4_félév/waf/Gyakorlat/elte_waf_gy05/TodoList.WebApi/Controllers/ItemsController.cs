using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoList.Persistence;
using TodoList.Persistence.DTO;
using TodoList.Persistence.Services;

namespace TodoList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly TodoListService _service;

        public ItemsController(TodoListService service)
        {
            _service = service;
        }

        // GET: api/Items
        [HttpGet]
        public ActionResult<IEnumerable<ItemDto>> GetItems(int listId)
        {
            List list;
            try
            {
                list = _service.GetListById(listId);
            }
            catch (Exception)
            {

                return NotFound();
            }

            return list.Items.Select(item => (ItemDto)item).ToList();
        }
    }
}
