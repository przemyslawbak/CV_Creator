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
        public List<CheckedProject> GetProjectList(List<Technology> tech, List<TechnologyProject> techProj)
        {
            return new List<CheckedProject>();
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
        public List<TechnologyProject> GetTechProjList()
        {
            return new List<TechnologyProject>(File.ReadAllLines(_inputProjectsTechnologiesFile)
                .Where(row => row.Contains("|"))
                .Select(row => new TechnologyProject()
                {
                    TechnologyID = int.Parse(row.Split('|')[0]),
                    ProjectID = int.Parse(row.Split('|')[1]),
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
