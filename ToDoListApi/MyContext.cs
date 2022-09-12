using Microsoft.EntityFrameworkCore;
using ToDoListApi.Models;

namespace ToDoListApi
{
    public class MyContext : DbContext
    {
        public MyContext()
        {
        }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<ToDoModel> ToDoModels { get; set; }
        public DbSet<GroupModel> GroupModel { get; set; }

        /// <summary>
        /// настройки подключения к базе данных
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = C:/Users/leont/Desktop/Work/ToDoListApi/ToDoDb.db;");
        }
    }
}
