using System.Collections.Generic;
using CV_Creator.Models;

namespace CV_Creator.Services
{
    public interface IHtmlStringSource
    {
        string GetHtmlDocumentOpened();
        string GetHtmlBodyOpened();
        string GetHtmlCvHeader(string name, string company, string position);
        string GetHtmlCvProjects(List<Project> loadedProjects);
        string GetHtmlCvTechnologies(List<Technology> loadedTechStack);
        string GetHtmlCvEmployment();
        string GetHtmlCvEducation();
        string GetHtmlCvInterests();
        string GetHtmlCvRodoFooter(string companyName);
        string GetHtmlBodyClosed();
        string GetHtmlDocumentClosed();
        string GetHtmlHead();
    }
}