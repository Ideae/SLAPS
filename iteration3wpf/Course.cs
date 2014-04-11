using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Course : Loadable<Course>
    {
        [Synchronize(true)]
        private int _Id;
        public override int Id { get {return syncDown("Id", _Id); } set { _Id = syncUp("Id", value); } }
        [Synchronize(true)]
        private string _CourseCode;
        public string CourseCode { get {return syncDown("CourseCode", _CourseCode); } set { _CourseCode = syncUp("CourseCode", value); } }
        [Synchronize]
        private User _Instructor;
        public User Instructor { get {return syncDown("Instructor", _Instructor); } set { _Instructor = syncUp("Instructor", value); } }
        [Synchronize]
        private List<Project> _Projects;
        public List<Project> Projects { get {return syncDown("Projects", _Projects); } set { _Projects = syncUp("Projects", value); } }
        [Synchronize]
        private List<Group> _Groups;
        public List<Group> Groups { get {return syncDown("Groups", _Groups); } set { _Groups = syncUp("Groups", value); } }
        [Synchronize]
        private List<User> _Students;
        public List<User> Students { get {return syncDown("Students", _Students); } set { _Students = syncUp("Students", value); } }




        public void addProject() { }

        public override string ToString()
        {
            return CourseCode;
        }
    }
}
