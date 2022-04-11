using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_Do_List_Backend.Domain;
using To_Do_List_Backend.Dto;
using To_Do_List_Backend.Services;
using To_Do_List_Backend.UnitOfWork;

namespace To_Do_List_Backend.Controllers
{
    [ApiController]
    [Route( "rest/[controller]" )]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IUnitOfWork _unitOfWork;

        public TodoController( ITodoService todoService, IUnitOfWork unitOfWork )
        {
            _todoService = todoService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route( "get-all" )]
        public ActionResult<List<TodoDto>> GetAll()
        {
            List<Todo> todos = _todoService.GetTodos();
            if ( todos.Count == 0 )
            {
                return NoContent();
            }

            return Ok( todos.Select( x => x.ToTodoDto() ) );
        }

        [HttpGet]
        [Route( "{todoId}" )]
        public ActionResult<TodoDto> GetTodo( int todoId )
        {
            Todo? todo = _todoService.GetTodo( todoId );
            if ( todo == null )
            {
                return NotFound();
            }
            return Ok( todo.ToTodoDto() );
        }

        [HttpPost]
        [Route( "create" )]
        public ActionResult<TodoDto> CreateTodo( [FromBody] TodoDto todoDto )
        {
            Todo? createdTodo = _todoService.CreateTodo( todoDto );
            if ( createdTodo == null )
            {
                return BadRequest();
            }

            _unitOfWork.Commit();

            return Ok( createdTodo.ToTodoDto() );
        }

        [HttpPut]
        [Route( "{todoId}/complete" )]
        public ActionResult<TodoDto> CompleteTodo( int todoId )
        {
            Todo? comletedTodo = _todoService.CompleteTodo( todoId );
            if ( comletedTodo == null )
            {
                return NotFound();
            }

            _unitOfWork.Commit();

            return Ok( comletedTodo );
        }

        [HttpDelete]
        [Route( "{todoId}/delete" )]
        public IActionResult DeleteTodo( int todoId )
        {
            _todoService.DeleteTodo( todoId );

            _unitOfWork.Commit();

            return NoContent();
        }
    }
}
