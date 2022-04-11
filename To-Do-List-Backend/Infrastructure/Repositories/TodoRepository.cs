using To_Do_List_Backend.Domain;
using To_Do_List_Backend.Infrastructure;

namespace To_Do_List_Backend.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _dbContext;

        public TodoRepository( TodoDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public Todo Create( Todo todo )
        {
            var entity = _dbContext.Set<Todo>().Add( todo );
            return entity.Entity;
        }

        public void Delete( Todo todo )
        {
            _dbContext.Set<Todo>().Remove( todo );
        }

        public Todo? Get( int id )
        {
            return _dbContext.Set<Todo>().FirstOrDefault( x => x.Id == id );
        }

        public List<Todo> GetTodos()
        {
            return _dbContext.Set<Todo>().ToList();
        }

        public Todo Update( Todo todo )
        {
            var entity = _dbContext.Update( todo );
            return entity.Entity;
        }
    }
}
