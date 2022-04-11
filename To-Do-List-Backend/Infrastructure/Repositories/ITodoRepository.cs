using To_Do_List_Backend.Domain;

namespace To_Do_List_Backend.Infrastructure.Repositories
{
    public interface ITodoRepository
    {
        List<Todo> GetTodos();
        Todo? Get( int id );
        Todo Create( Todo todo );
        void Delete( Todo todo );
        Todo Update( Todo todo );
    }
}
