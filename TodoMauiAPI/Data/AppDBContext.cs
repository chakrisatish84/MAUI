using Microsoft.EntityFrameworkCore;
using TodoMauiAPI.Models;

namespace TodoMauiAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Todo> ToDos => Set<Todo>();
    }
}

