using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Course: Loadable<Course>
    {
        [Synchronize]
        private int _Id;
        public override int Id { get { return _Id; } set { _Id = sync("Id", value); } }
        [Synchronize]
        private string _CourseCode;
        public string CourseCode { get { return _CourseCode; } set { _CourseCode = sync("CourseCode", value); } }
        [Synchronize]
        private User _Instructor;
        public User Instructor { get { return _Instructor; } set { _Instructor = sync("Instructor", value); } }
        [Synchronize]
        private List<Project> _Projects;
        public List<Project> Projects { get { return _Projects; } set { _Projects = sync("Projects", value); } }
        [Synchronize]
        private List<Group> _Groups;
        public List<Group> Groups { get { return _Groups; } set { _Groups = sync("Groups", value); } }
        [Synchronize]
        private List<User> _Students;
        public List<User> Students { get { return _Students; } set { _Students = sync("Students", value); } }




        public void addProject() { }

        public override string ToString()
        {
            return CourseCode;
        }
    }
}
