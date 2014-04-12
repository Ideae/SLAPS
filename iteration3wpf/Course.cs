using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private string _Title;
        public string Title { get { return syncDown("Title", _Title); } set { _Title = syncUp("Title", value); } }
        [Synchronize]
        private string _Description;
        public string Description { get { return syncDown("Description", _Description); } set { _Description = syncUp("Description", value); } }
        [Synchronize]
        private User _Instructor;
        public User Instructor { get {return syncDown("Instructor", _Instructor); } set { _Instructor = syncUp("Instructor", value); } }
        [Synchronize]
        private ObservableCollection<Project> _Projects = new ObservableCollection<Project>();
        public ObservableCollection<Project> Projects { get {return syncDown("Projects", _Projects); } set { _Projects = syncUp("Projects", value); } }
        [Synchronize]
        private ObservableCollection<Group> _Groups = new ObservableCollection<Group>();
        public ObservableCollection<Group> Groups { get {return syncDown("Groups", _Groups); } set { _Groups = syncUp("Groups", value); } }
        [Synchronize]
        private ObservableCollection<User> _Students = new ObservableCollection<User>();
        public ObservableCollection<User> Students { get {return syncDown("Students", _Students); } set { _Students = syncUp("Students", value); } }
        [Synchronize]
        private ObservableCollection<Message> _Announcements = new ObservableCollection<Message>();
        public ObservableCollection<Message> Announcements { get { return syncDown("Announcements", _Announcements); } set { _Announcements = syncUp("Announcements", value); } }
        [Synchronize]
        private DateTime _DateStart;
        public DateTime DateStart { get { return syncDown("DateStart", _DateStart); } set { _DateStart = syncUp("DateStart", value); } }
        [Synchronize]
        private DateTime _DateEnd;
        public DateTime DateEnd { get { return syncDown("DateEnd", _DateEnd); } set { _DateEnd = syncUp("DateEnd", value); } }
        [Synchronize]
        private DateTime _DateEnrollment;
        public DateTime DateEnrollment { get { return syncDown("DateEnrollment", _DateEnrollment); } set { _DateEnrollment = syncUp("DateEnrollment", value); } }
        protected Course(int id) : base(id) 
        {
            _Id = id;
            _Projects.CollectionChanged += delegate { Projects = _Projects; };
            _Groups.CollectionChanged += delegate { Groups = _Groups; };
            _Students.CollectionChanged += delegate { Students = _Students; };
            _Announcements.CollectionChanged += delegate { Announcements = _Announcements; };

        }

        public void addProject() { }

        public override string ToString()
        {
            return CourseCode;
        }
    }
}
