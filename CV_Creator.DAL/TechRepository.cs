using System.Collections.Generic;
using System.Threading.Tasks;
using CV_Creator.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CV_Creator.DAL
{
    public class TechRepository : ITechRepository
    {
        private readonly ProjectsDbContext _context;

        public TechRepository(ProjectsDbContext context)
        {
            _context = context;
        }

        public async Task<List<CheckedTech>> GetAllCheckedTechnologiesAsync()
        {
            return await(from q in _context.Technologies
                          select new CheckedTech
                          {
                              TechnologyID = q.TechnologyID,
                              Checked = false,
                              Name = q.Name
                          }).ToListAsync();
        }

        public object GetTechnologiesFromChecked(List<CheckedTech> list)
        {
            var ids = list.Select(item => item.TechnologyID).ToList();
            IQueryable<Technology> query = _context.Technologies
                .Where(tech => ids.Contains(tech.TechnologyID))
                .OrderByDescending(tech => tech.Importance)
                .ThenByDescending(tech => tech.Level);
            return query.ToList();
        }
    }
}
