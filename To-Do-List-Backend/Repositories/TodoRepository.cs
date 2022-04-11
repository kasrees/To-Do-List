using To_Do_List_Backend.Domain;
using To_Do_List_Backend.Infrastructure;

namespace To_Do_List_Backend.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _dbContext;

        public TodoRepository( TodoDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public void Create( Todo todo )
        {
            _dbContext.Set<Todo>().Add( todo );
        }

        public void Delete( Todo todo )
        {
            var entity = Get( todo.Id );
            if (entity == null)
            {
                return;
            }

            _dbContext.Set<Todo>().Remove( entity );
        }

        public Todo? Get( int id )
        {
            return _dbContext.Set<Todo>().FirstOrDefault( x => x.Id == id );
        }

        public List<Todo> GetTodos()
        {
            return _dbContext.Set<Todo>().ToList();
        }

        public void Update( Todo todo )
        {
            var entity = Get( todo.Id );
            if (entity == null)
            {
                return;
            }
            entity.Title = todo.Title;
            entity.IsDone = todo.IsDone;
            _dbContext.Update( entity );
        }
    }
}
