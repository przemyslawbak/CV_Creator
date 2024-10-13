using CV_Creator.Models;
using System.Collections.Generic;
using System.Linq;

namespace CV_Creator.Services
{
    public class TechStackProcessor : ITechStackProcessor
    {
        private readonly int _maxTechStackty = 12;

        public List<Technology> SelectTechStack(List<Project> loadedProjects) //null?
        {
            List<Technology> allTech = loadedProjects
                .Select(proj => proj.TechnologiesProjects)
                .SelectMany(tech => tech)
                .Select(tech => tech.Technology)
                .Distinct()
                .OrderByDescending(tech => tech.Importance)
                .ThenByDescending(tech => tech.Level)
                .ToList();

            if (allTech.Count() > _maxTechStackty)
            {
                allTech = allTech.Take(12).ToList();
            }

            return allTech;
        }
    }
}
