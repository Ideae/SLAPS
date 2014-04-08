using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Course
    {
        public string name { get; set; }
        public Instructor instructor { get; set; }
        public List<Student> enrolledStudents { get; set; }
        public List<Group> groups { get; set; }
        public List<Project> assignedProjects { get; set; }
        public void addProject() { }

        public override string ToString()
        {
            return name;
        }
    }
}
