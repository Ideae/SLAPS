using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Instructor : User
    {
        //File[] getInstructionDocs();
        public List<Course> courses { get; set; }
        //List<Project> getProjects() { }
        //List<Submission> getSubmissions() { }
        public void requestPWReset() { }
    }
}
