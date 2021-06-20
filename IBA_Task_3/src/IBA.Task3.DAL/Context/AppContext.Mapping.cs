using Microsoft.EntityFrameworkCore;

namespace IBA.Task3.DAL.Context
{
    /// <summary>
    /// Test context
    /// </summary>
    public partial class AppContext : DbContext
    {
        /// <summary>
        /// Подключение основного маппинга сущностей.
        /// </summary>
        /// <param name="builder">Объект построитель связей для контекста.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Models.TestAssignment>().HasOne(t => t.Test).WithMany().HasForeignKey(x => x.TestId);
            

            base.OnModelCreating(builder);
        }
    }
}