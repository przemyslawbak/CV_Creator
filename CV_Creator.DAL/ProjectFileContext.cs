using CV_Creator.Models;
using CV_Creator.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CV_Creator.DAL
{
    public class ProjectFileContext : IFileContext
    {
        private readonly IFileManager _fileManager;

        public ProjectFileContext(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public List<CheckedProject> GetAllProjects()
        {
            List<Technology> tech = _fileManager.GetTechnologyList();
            List<TechnologyProject> techProj = _fileManager.GetTechProjList();
            return _fileManager.GetProjectList(tech, techProj);
        }
    }
}
