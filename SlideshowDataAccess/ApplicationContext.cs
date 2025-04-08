using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SlideshowDataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}