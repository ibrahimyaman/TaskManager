using Microsoft.EntityFrameworkCore;
using TaskManager.Entities.Concrete;

namespace TaskManager.DataAccess.Concrete.EntityFramework.Extentions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImportanceType>().HasData(
                new ImportanceType { Id = 1, Description = "Önemli - Acil" },
                new ImportanceType { Id = 2, Description = "Önemli - Acil Değil" },
                new ImportanceType { Id = 3, Description = "Önemli Değil - Acil" },
                new ImportanceType { Id = 4, Description = "Önemli Değil - Acil Değil" }
            );
        }
    }
}
