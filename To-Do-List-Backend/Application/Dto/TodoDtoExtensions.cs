using To_Do_List_Backend.Domain;

namespace To_Do_List_Backend.Application.Dto
{
    public static class TodoDtoExtensions
    {
        public static Todo ToTodo( this TodoDto dto )
        {
            return new Todo
            {
                Title = dto.Title,
                IsDone = dto.IsDone,
            };
        }
    }
}
