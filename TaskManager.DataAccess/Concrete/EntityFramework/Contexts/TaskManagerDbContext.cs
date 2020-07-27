using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities.Concrete;
using TaskManager.DataAccess.Concrete.EntityFramework.Extentions;
using TaskManager.Entities.Concrete;

namespace TaskManager.DataAccess.Concrete.EntityFramework.Contexts
{
    public partial class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext()
        {
        }

        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DailyPlan> DailyPlans { get; set; }
        public virtual DbSet<DailyPlanDetail> DailyPlanDetails { get; set; }
        public virtual DbSet<ImportanceType> ImportanceTypes { get; set; }
        public virtual DbSet<MonthlyPlan> MonthlyPlans { get; set; }
        public virtual DbSet<MonthlyPlanDetail> MonthlyPlanDetails { get; set; }
        public virtual DbSet<OperationClaim> OperationClaims { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public virtual DbSet<WeeklyPlan> WeeklyPlans { get; set; }
        public virtual DbSet<WeeklyPlanDetail> WeeklyPlanDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=TaskManager;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyPlan>(entity =>
            {
                entity.HasIndex(e => e.ImportanceTypeId);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ImportanceType)
                    .WithMany(p => p.DailyPlans)
                    .HasForeignKey(d => d.ImportanceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyPlans_ImportanceTypes");
            });

            modelBuilder.Entity<DailyPlanDetail>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.DailyPlan)
                    .WithMany(p => p.DailyPlanDetails)
                    .HasForeignKey(d => d.DailyPlanId)
                    .HasConstraintName("FK_DailyPlanDetails_DailyPlans");
            });

            modelBuilder.Entity<ImportanceType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MonthlyPlan>(entity =>
            {
                entity.HasIndex(e => e.ImportanceTypeId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ImportanceType)
                    .WithMany(p => p.MonthlyPlans)
                    .HasForeignKey(d => d.ImportanceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MonthlyPlans_ImportanceTypes");
            });

            modelBuilder.Entity<MonthlyPlanDetail>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasOne(d => d.MonthlyPlan)
                    .WithMany(p => p.MonthlyPlanDetails)
                    .HasForeignKey(d => d.MonthlyPlanId)
                    .HasConstraintName("FK_MonthlyPlanDetails_MonthlyPlans");
            });

            modelBuilder.Entity<OperationClaim>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(40);
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshTokens_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(175);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(5000);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(5000);

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(75);
            });

            modelBuilder.Entity<UserOperationClaim>(entity =>
            {
                entity.HasIndex(e => e.OperationClaimId);

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.OperationClaim)
                    .WithMany(p => p.UserOperationClaims)
                    .HasForeignKey(d => d.OperationClaimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserOperationClaims_OperationClaims");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserOperationClaims)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserOperationClaims_Users");
            });

            modelBuilder.Entity<WeeklyPlan>(entity =>
            {
                entity.HasIndex(e => e.ImportanceTypeId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ImportanceType)
                    .WithMany(p => p.WeeklyPlans)
                    .HasForeignKey(d => d.ImportanceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeeklyPlans_ImportanceTypes");
            });

            modelBuilder.Entity<WeeklyPlanDetail>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasOne(d => d.WeeklyPlan)
                    .WithMany(p => p.WeeklyPlanDetails)
                    .HasForeignKey(d => d.WeeklyPlanId)
                    .HasConstraintName("FK_WeeklyPlanDetails_WeeklyPlans");
            });

            modelBuilder.Seed();
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}