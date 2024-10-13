using CV_Creator.Models;
using CV_Creator.Services;
using System.Collections.Generic;

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
            List<TechnologyProject> techProj = _fileManager.GetTechProjList(tech);
            return _fileManager.GetProjectList(tech, techProj);
        }

        public List<Project> GetCheckedProjects(List<int> ids)
        {
            List<Technology> tech = _fileManager.GetTechnologyList();
            List<TechnologyProject> techProj = _fileManager.GetTechProjList(tech);
            return _fileManager.GetCheckedProjectList(tech, techProj, ids);
        }
    }
}
