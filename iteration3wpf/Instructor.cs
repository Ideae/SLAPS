using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Instructor : User
    {
        public Instructor(DataRow UserRow)
        {
            SetData(UserRow);
        }
        //File[] getInstructionDocs();
        //List<Project> getProjects() { }
        //List<Submission> getSubmissions() { }
        public void requestPWReset() { }
    }
}
