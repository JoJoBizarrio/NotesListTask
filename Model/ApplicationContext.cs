using Microsoft.EntityFrameworkCore;

namespace NotesListTask.Model
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Note> Notes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }
    }
}