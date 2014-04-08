using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Project
    {
        public string name { get; set; }
        public string description { get; set; }
        public List<Submission> groupSubmissions { get; set; }
        //File[] InstructionDocs;

        public void edit() { }
        public override string ToString()
        {
            return name;
        }
        
    }
}
