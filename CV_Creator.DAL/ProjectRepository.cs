using System.Collections.Generic;
using CV_Creator.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Creator.DAL
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectsDbContext _context;
        public ProjectRepository(ProjectsDbContext context)
        {
            _context = context;
        }
        public async Task<List<CheckedProject>> GetAllCheckedProjectsAsync()
        {
            return await (from q in _context.Projects.Include(i => i.TechnologiesProjects).ThenInclude(techproj => techproj.Technology)
                          select new CheckedProject
                          {
                              ProjectID = q.ProjectID,
                              Checked = false,
                              Name = q.Name,
                              Comment = q.Comments,
                              Techs = string.Join(", ", q.TechnologiesProjects.Select(sn => sn.Technology.Name).ToArray())
                          }).ToListAsync();
        }

        public object GetProjectsFromChecked(List<CheckedProject> list)
        {
            var ids = list.Select(item => item.ProjectID).ToList();
            IQueryable<Project> query = _context.Projects
                .OrderBy(project => project.CompletionDate)
                .Where(project => ids.Contains(project.ProjectID))
                .Include(project => project.TechnologiesProjects)
                .ThenInclude(techproj => techproj.Technology);
            return query.ToList();
        }
    }
}
