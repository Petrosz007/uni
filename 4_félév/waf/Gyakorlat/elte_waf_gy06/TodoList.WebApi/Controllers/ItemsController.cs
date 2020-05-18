using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            catch (InvalidOperationException)
            {

                return NotFound();
            }

            return list.Items.Select(item => (ItemDto)item).ToList();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Int32 id)
        {
            try
            {
                return (ItemDto)_service.GetItem(id);
            }
            catch (InvalidOperationException)
            {

                return NotFound();
            }
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutItem(Int32 id, ItemDto item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            if (_service.UpdateItem((Item)item))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Items
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPost]
        public ActionResult<ListDto> PostItem(ItemDto itemDto)
        {
            var item = _service.CreateItem((Item)itemDto);
            if (item is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetItem), new { id = item.Id }, (ItemDto)item);
            }
        }

        // DELETE: api/Lists/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            if (_service.DeleteItem(id))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
