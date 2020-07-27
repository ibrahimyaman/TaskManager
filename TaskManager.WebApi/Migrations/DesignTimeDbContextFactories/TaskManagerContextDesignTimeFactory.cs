using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;
using TaskManager.DataAccess.Concrete.EntityFramework.Contexts;

namespace TaskManager.WebApi.Migrations.DesignTimeDbContextFactories
{
    public class TaskManagerContextDesignTimeFactory : IDesignTimeDbContextFactory<TaskManagerDbContext>
    {
        public TaskManagerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskManagerDbContext>()
              .UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=TaskManager;Integrated Security=True", bo => bo.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));
            return new TaskManagerDbContext(optionsBuilder.Options);
        }
    }
}
