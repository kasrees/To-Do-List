using To_Do_List_Backend.Infrastructure;

namespace To_Do_List_Backend.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoDbContext _dbContext;

        public UnitOfWork( TodoDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
