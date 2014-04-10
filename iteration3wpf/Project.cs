using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Project: Loadable<Project>
    {
        [Synchronize]
        private int _Id;
        public int Id { get { return _Id; } set { _Id = sync("Id", value); } }
        [Synchronize]
        private string _Title;
        public string Title { get { return _Title; } set { _Title = sync("Title", value); } }
        [Synchronize]
        private string _Summary;
        public string Summary { get { return _Summary; } set { _Summary = sync("Summary", value); } }
        [Synchronize]
        private List<Group> _Groups;
        public List<Group> Groups { get { return _Groups; } set { _Groups = sync("Groups", value); } }
        [Synchronize]
        private Course _PrjCourse;
        public Course PrjCourse { get { return _PrjCourse; } set { _PrjCourse = sync("PrjCourse", value); } }
        [Synchronize]
        private float _MaxMarks;
        public float MaxMarks { get { return _MaxMarks; } set { _MaxMarks = sync("MaxMarks", value); } }
        [Synchronize]
        private List<LFile> _Attatchments;
        public List<LFile> Attatchments { get { return _Attatchments; } set { _Attatchments = sync("Attatchments", value); } }
        
        
        public override string ToString()
        {
            return name;
        }
        
    }
}
