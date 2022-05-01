using CV_Creator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CV_Creator.Services
{
    public interface ITechStackProcessor
    {
        List<Technology> SelectTechStack(List<Project> loadedProjects);
    }
}