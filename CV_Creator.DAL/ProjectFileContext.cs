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

        public Task<List<CheckedProject>> GetAllProjects()
        {
            List<CheckedProject> projects = new List<CheckedProject>();

        }
    }
}
