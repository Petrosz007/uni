using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TodoList.Persistence;
using TodoList.Persistence.DTO;
using TodoList.Persistence.Services;

namespace TodoList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly TodoListService _service;

        public ListsController(TodoListService service)
        {
            _service = service;
        }

        // GET: api/Lists
        [HttpGet]
        public ActionResult<IEnumerable<ListDto>> GetLists()
        {
            return _service.GetLists().Select(list => (ListDto)list).ToList();
        }

        // GET: api/Lists/5
        [HttpGet("{id}")]
        public ActionResult<ListDto> GetList(Int32 id)
        {
            try
            {
                return (ListDto)_service.GetListById(id);
            }
            catch (InvalidOperationException)
            {

                return NotFound();
            }
        }

        // PUT: api/Lists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutList(Int32 id, ListDto list)
        {
            if (id != list.Id)
            {
                return BadRequest();
            }

            if (_service.UpdateList((List)list))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Lists
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPost]
        public ActionResult<ListDto> PostList(ListDto listDto)
        {
            var list = _service.CreateList((List)listDto);
            if (list is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetList), new { id = list.Id }, (ListDto)list);
            }
        }

        // DELETE: api/Lists/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteList(Int32 id)
        {
            if (_service.DeleteList(id))
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
