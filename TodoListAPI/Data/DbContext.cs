using Microsoft.EntityFrameworkCore;
using TodoListAPI.Models;

namespace TodoListAPI.Data
{
    public class AppDbContext : DbContext
    {
        // ✅ This constructor is required when you use AddDbContext in Program.cs
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // ✅ This will become your Tasks table
        public DbSet<ToDoTasks> ToDoTasks { get; set; } = null!;
    }
}
