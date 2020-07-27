using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.DataAccess.Concrete.EntityFramework.Contexts;

namespace TaskManager.WebApi.Migrations.Utilities
{
    public class MigrationHelper
    {
        public static void MigrateDatabases()
        {
            using (var dbContext = new TaskManagerDbContext(GetDbContextOptions<TaskManagerDbContext>()))
            {
                dbContext.Database.Migrate();
            }
        }
        public static async Task MigrateDatabasesAsync()
        {
            using (var dbContext = new TaskManagerDbContext(GetDbContextOptions<TaskManagerDbContext>()))
            {
                await dbContext.Database.MigrateAsync();
            }
        }

        private static DbContextOptions<T> GetDbContextOptions<T>() where T : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>()
                    .UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=TaskManager;Integrated Security=True", bo => bo.MigrationsAssembly(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name));

            return optionsBuilder.Options;
        }
    }
}
