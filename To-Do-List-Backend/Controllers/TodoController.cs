using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_Do_List_Backend.Application.Dto;
using To_Do_List_Backend.Domain;
using To_Do_List_Backend.Infrastructure.Repositories;
using To_Do_List_Backend.UnitOfWork;

namespace To_Do_List_Backend.Controllers
{
    [ApiController]
    [Route( "rest/[controller]" )]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TodoController( ITodoRepository todoRepository, IUnitOfWork unitOfWork )
        {
            _todoRepository = todoRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route( "get-all" )]
        public ActionResult<List<TodoDto>> GetAll()
        {
            List<Todo> todos = _todoRepository.GetTodos();
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
            Todo? todo = _todoRepository.Get( todoId );
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
            var createdTodo = _todoRepository.Create( todoDto.ToTodo() );

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
            Todo? comletedTodo = _todoRepository.Get( todoId );
            if ( comletedTodo == null )
            {
                return NotFound();
            }

            comletedTodo.IsDone = true;
            _todoRepository.Update( comletedTodo );

            _unitOfWork.Commit();

            return Ok( comletedTodo.ToTodoDto() );
        }

        [HttpDelete]
        [Route( "{todoId}/delete" )]
        public IActionResult DeleteTodo( int todoId )
        {
            var entity = _todoRepository.Get( todoId );

            if(entity == null)
            {
                return BadRequest();
            }

            _todoRepository.Delete( entity );

            _unitOfWork.Commit();

            return NoContent();
        }
    }
}
