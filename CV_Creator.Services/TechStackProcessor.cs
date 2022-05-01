using CV_Creator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Creator.Services
{
    public class TechStackProcessor : ITechStackProcessor
    {
        public List<Technology> SelectTechStack(List<Project> loadedProjects)
        {
            List<Technology> allTech = loadedProjects
                .Select(proj => proj.TechnologiesProjects)
                .SelectMany(tech => tech)
                .Select(tech => tech.Technology)
                .Distinct()
                .OrderByDescending(tech => tech.Importance)
                .ThenByDescending(tech => tech.Level)
                .ToList();

            return allTech;
        }
    }
}
