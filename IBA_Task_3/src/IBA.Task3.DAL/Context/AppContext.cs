using IBA.Task3.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace IBA.Task3.DAL.Context
{
    /// <summary>
    /// Test context
    /// </summary>
    public partial class AppContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestAssignment> TestAssignments { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<TestResult> TestResults { get; set; }

        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}