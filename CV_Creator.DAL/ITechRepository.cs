using System.Collections.Generic;
using System.Threading.Tasks;
using CV_Creator.Models;

namespace CV_Creator.DAL
{
    public interface ITechRepository
    {
        Task<List<CheckedTech>> GetAllCheckedTechnologiesAsync();
        object GetTechnologiesFromChecked(List<CheckedTech> list);
    }
}