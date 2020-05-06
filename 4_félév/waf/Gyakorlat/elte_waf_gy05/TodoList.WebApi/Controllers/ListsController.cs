using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
    }
}
