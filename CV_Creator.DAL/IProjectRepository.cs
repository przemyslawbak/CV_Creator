using CV_Creator.Models;
using System.Collections.Generic;

namespace CV_Creator.DAL
{
    public interface IProjectRepository
    {
        List<CheckedProject> GetAllCheckedProjects();
        object GetProjectsFromChecked(List<CheckedProject> list);
    }
}
