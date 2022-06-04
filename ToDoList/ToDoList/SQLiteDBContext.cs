using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList
{
    public class SQLiteDBContext: DbContext
    {
        public DbSet<TodoItem> Todo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=ToDo.db");
    }
}
