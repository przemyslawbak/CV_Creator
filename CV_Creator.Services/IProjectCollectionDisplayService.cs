using System.Collections.Generic;
using CV_Creator.Models;

namespace CV_Creator.Services
{
    public interface IProjectCollectionDisplayService
    {
        List<CheckedProject> GetDisplayResults(int currentPage, int displayItemsPerPage, List<CheckedProject> filteredProjects);
        List<CheckedProject> FilterDisplayedCollection(string filterTechPhrase, List<CheckedProject> filteredProjects, List<CheckedProject> loadedAllProjects, int _displayItemsPerPage, int currentPage);
        int GetPagesCount(int filteredProjectsCount, int displayItemsPerPage);
    }
}