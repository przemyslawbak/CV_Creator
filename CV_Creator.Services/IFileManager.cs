using CV_Creator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CV_Creator.Services
{
    public interface IFileManager
    {
        List<Project> GetCheckedProjectList(List<Technology> tech, List<TechnologyProject> techProj, List<int> ids);
        List<CheckedProject> GetProjectList(List<Technology> tech, List<TechnologyProject> techProj);
        List<Technology> GetTechnologyList();
        List<TechnologyProject> GetTechProjList(List<Technology> tech);
        Task SaveToDiskAsync(byte[] pdfCv, string filePath);
    }
}