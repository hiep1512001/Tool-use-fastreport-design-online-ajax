using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace use_open_source_fast_report.Models
{
    public class Context:DbContext
    {
        public  Context(DbContextOptions options) : base(options) { }
        # region DBet
        public DbSet<Report> Reports { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>(b =>
            {
                b.HasKey(e => e.ID);
                b.Property(e => e.ID).ValueGeneratedOnAdd();
            });
        }
    }
}
