using CV_Creator.Models;
using Microsoft.EntityFrameworkCore;

namespace CV_Creator.DAL
{
    public class ProjectsDbContext : DbContext
    {
        public ProjectsDbContext(DbContextOptions<ProjectsDbContext> options) : base(options) { }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Literature> Literatures { get; set; }
        public DbSet<LiteratureTechnology> LiteraturesTech { get; set; }
        public DbSet<TechnologyProject> TechProjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LiteratureTechnology>()
                .HasKey(c => new { c.LiteratureID, c.TechnologyID });

            modelBuilder.Entity<TechnologyProject>()
                .HasKey(c => new { c.TechnologyID, c.ProjectID });
        }
    }
}
