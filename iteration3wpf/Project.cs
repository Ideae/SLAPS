using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Project: Loadable<Project>
    {
        [Synchronize(true)]
        private int _Id;
        public override int Id { get {return syncDown("Id", _Id); } set { _Id = syncUp("Id", value); } }
        [Synchronize(true)]
        private string _Title;
        public string Title { get {return syncDown("Title", _Title); } set { _Title = syncUp("Title", value); } }
        [Synchronize]
        private string _Summary;
        public string Summary { get {return syncDown("Summary", _Summary); } set { _Summary = syncUp("Summary", value); } }
        [Synchronize]
        private List<Group> _Groups;
        public List<Group> Groups { get {return syncDown("Groups", _Groups); } set { _Groups = syncUp("Groups", value); } }
        [Synchronize]
        private Course _PrjCourse;
        public Course PrjCourse { get {return syncDown("PrjCourse", _PrjCourse); } set { _PrjCourse = syncUp("PrjCourse", value); } }
        [Synchronize]
        private float _MaxMarks;
        public float MaxMarks { get {return syncDown("MaxMarks", _MaxMarks); } set { _MaxMarks = syncUp("MaxMarks", value); } }
        [Synchronize]
        private List<LFile> _Attatchments;
        public List<LFile> Attatchments { get {return syncDown("Attatchments", _Attatchments); } set { _Attatchments = syncUp("Attatchments", value); } }
        
        
        public override string ToString()
        {
            return Title;
        }
        
    }
}
