using System;
using System.Collections.Generic;
using System.Text;

namespace CV_Creator.Models
{
    public class CheckedProject
    {
        public int ProjectID { get; set; }
        public bool Checked { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Techs { get; set; }
    }
}
