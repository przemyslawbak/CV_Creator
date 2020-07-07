using CV_Creator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CV_Creator.DAL
{
    public interface IProjectRepository
    {
        Task<List<CheckedProject>> GetAllCheckedProjectsAsync();
        object GetProjectsFromChecked(List<CheckedProject> list);
    }
}
