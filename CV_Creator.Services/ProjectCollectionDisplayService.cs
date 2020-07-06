using CV_Creator.Models;
using System.Collections.Generic;
using System.Linq;

namespace CV_Creator.Services
{
    public class ProjectCollectionDisplayService : IProjectCollectionDisplayService
    {
        public List<CheckedProject> GetDisplayResults(int currentPage, int displayItemsPerPage, List<CheckedProject> filteredProjects)
        {
            int skip = (currentPage - 1) * displayItemsPerPage;

            return new List<CheckedProject>(filteredProjects.Skip(skip).Take(displayItemsPerPage));
        }

        public List<CheckedProject> FilterDisplayedCollection(string filterTechPhrase, List<CheckedProject> filteredProjects, List<CheckedProject> loadedAllProjects, int displayItemsPerPage, int currentPage)
        {
            if (!string.IsNullOrEmpty(filterTechPhrase))
            {
                filteredProjects = loadedAllProjects.Where(a => a.Techs.ToLower().Contains(filterTechPhrase.ToLower())).ToList();
            }
            else
            {
                filteredProjects = loadedAllProjects;
            }

            return filteredProjects;
        }

        public int GetPagesCount(int filteredProjectsCount, int displayItemsPerPage)
        {
            return (filteredProjectsCount + displayItemsPerPage - 1) / displayItemsPerPage;
        }
    }
}
