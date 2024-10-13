using CV_Creator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Creator.Services
{
    public class FileManager : IFileManager
    {
        private readonly string _inputTechnologiesFile = "_tech.txt";
        private readonly string _inputProjectsFile = "_proj.txt";
        private readonly string _inputProjectsTechnologiesFile = "_tech_proj.txt";

        //todo future: async
        public List<Project> GetCheckedProjectList(List<Technology> tech, List<TechnologyProject> techProj, List<int> ids)
        {
            List<Project> result = new List<Project>(File.ReadAllLines(_inputProjectsFile)
                .Where(row => row.Contains("|"))
                .Select(row => new Project()
                {
                    ProjectID = int.Parse(row.Split('|')[0]),
                    Name = row.Split('|')[1],
                    Comments = row.Split('|')[2],
                    GitHubUrl = row.Split('|')[3],
                    WebUrl = row.Split('|')[4],
                    WorkLogUrl = row.Split('|')[5],
                    YouTubeUrl = row.Split('|')[6],
                    BackColor = row.Split('|')[7],
                    PictureUrl = row.Split('|')[8],
                    CompletionDate = DateTime.Parse(row.Split('|')[9]),
                    DesignPattern = row.Split('|')[10],
                    Tests = bool.Parse(row.Split('|')[11]),
                    TechnologiesProjects = techProj.Where(t => t.ProjectID == int.Parse(row.Split('|')[0])).ToList()
                }))
                .Where(p => ids.Any(i => i == p.ProjectID))
                .ToList();

            return result;
        }

        //todo future: async
        public List<CheckedProject> GetProjectList(List<Technology> tech, List<TechnologyProject> techProj)
        {
            List<CheckedProject> result = new List<CheckedProject>(File.ReadAllLines(_inputProjectsFile)
                .Where(row => row.Contains("|"))
                .Select(row => new CheckedProject()
                {
                    ProjectID = int.Parse(row.Split('|')[0]),
                    Name = row.Split('|')[1],
                    Comment = row.Split('|')[2],
                    Checked = false
                }));

            foreach (var proj in result)
            {
                proj.Techs = GetTechNames(proj.ProjectID, techProj, tech);
            }

            return result;
        }

        private string GetTechNames(int projectID, List<TechnologyProject> techProj, List<Technology> tech)
        {
            IEnumerable<int> nos = techProj.Where(x => x.ProjectID == projectID).Select(x => x.TechnologyID);
            string[] techNames = tech.Where(x => nos.Any(a => a == x.TechnologyID)).Select(x => x.Name).ToArray();
            return string.Join(", ", techNames);
        }

        //todo future: async
        public List<Technology> GetTechnologyList()
        {
            return new List<Technology>(File.ReadAllLines(_inputTechnologiesFile)
                .Where(row => row.Contains("|"))
                .Select(row => new Technology()
                {
                    TechnologyID = int.Parse(row.Split('|')[0]),
                    Name = row.Split('|')[1],
                    PictureFile = row.Split('|')[2],
                    Level = int.Parse(row.Split('|')[3]),
                    Importance = int.Parse(row.Split('|')[4]),
                }));
        }

        //todo future: async
        public List<TechnologyProject> GetTechProjList(List<Technology> tech)
        {
            return new List<TechnologyProject>(File.ReadAllLines(_inputProjectsTechnologiesFile)
                .Where(row => row.Contains("|"))
                .Select(row => new TechnologyProject()
                {
                    TechnologyID = int.Parse(row.Split('|')[0]),
                    ProjectID = int.Parse(row.Split('|')[1]),
                    Technology = tech.Where(t => t.TechnologyID == int.Parse(row.Split('|')[0])).FirstOrDefault()
                }));
        }

        //todo: async
        public async Task SaveToDiskAsync(byte[] pdfCv, string filePath)
        {
            try
            {
                using (FileStream sourceStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                {
                    await sourceStream.WriteAsync(pdfCv, 0, pdfCv.Length);
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
            }
        }
    }
}
