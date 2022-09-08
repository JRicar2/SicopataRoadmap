using Microsoft.EntityFrameworkCore;
using ToDoEntity.Models;

namespace ToDoEntity.Data
{
    public class ToDoDBContext : DbContext
    {
        public ToDoDBContext(DbContextOptions<ToDoDBContext> options) : base(options) { }

        public DbSet<ToDo> Tasks { get; set; }
    }
}
