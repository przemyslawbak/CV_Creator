using CV_Creator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CV_Creator.DAL
{
    public interface IProjectRepository
    {
        List<CheckedProject> GetAllCheckedProjectsAsync();
        List<Project> GetProjectsFromChecked(List<CheckedProject> list);
    }
}
