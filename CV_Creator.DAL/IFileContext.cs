using CV_Creator.Models;
using System.Collections.Generic;

namespace CV_Creator.DAL
{
    public interface IFileContext
    {
        List<CheckedProject> GetAllProjects();
        List<Project> GetCheckedProjects(List<int> ids);
    }
}