using CV_Creator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CV_Creator.Services
{
    public interface IFileManager
    {
        List<CheckedProject> GetProjectList(List<Technology> tech, List<TechnologyProject> techProj);
        List<Technology> GetTechnologyList();
        List<TechnologyProject> GetTechProjList();
        Task SaveToDiskAsync(byte[] pdfCv, string filePath);
    }
}