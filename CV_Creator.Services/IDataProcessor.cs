using System.Collections.Generic;
using CV_Creator.Models;

namespace CV_Creator.Services
{
    public interface IDataProcessor
    {
        byte[] ProcessPortfolio(List<Project> loadedProjects, string companyName, string positionApplied);
    }
}