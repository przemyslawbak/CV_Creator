using System;
using System.Collections.Generic;
using System.Text;

namespace CV_Creator.Models
{
    public class Technology
    {
        public int TechnologyID { get; set; }
        public string Name { get; set; }
        public string PictureFile { get; set; }
        public int Importance { get; set; }
        public ICollection<TechnologyProject> TechnologiesProjects { get; set; }
    }
}
