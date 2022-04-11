using Microsoft.EntityFrameworkCore;
using To_Do_List_Backend.Infrastructure.Configurations;

namespace To_Do_List_Backend.Infrastructure
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext( DbContextOptions options ) : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.ApplyConfiguration( new TodoConfiguration() );
        }
    }
}
