using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Group : Loadable<Group>
    {
        [Synchronize(true)]
        private int _Id;
        public override int Id { get {return syncDown("Id", _Id); } set { _Id = syncUp("Id", value); } }
        [Synchronize(true)]
        private string _Name;
        public string Name { get {return syncDown("Name", _Name); } set { _Name = syncUp("Name", value); } }
        [Synchronize]
        private List<User> _Members;
        public List<User> Members { get {return syncDown("Members", _Members); } set { _Members = syncUp("Members", value); } }
        [Synchronize]
        private Project _GrpProject;
        public Project GrpProject { get {return syncDown("GrpProject", _GrpProject); } set { _GrpProject = syncUp("Project", value); } }
        [Synchronize]
        private float _Mark;
        public float Mark { get {return syncDown("Mark", _Mark); } set { _Mark = syncUp("Mark", value); } }
        [Synchronize]
        private Submission _GrpSubmission;
        public Submission GrpSubmission { get {return syncDown("GrpSubmission", _GrpSubmission); } set { _GrpSubmission = syncUp("Submission", value); } }

        
        public Group() { }
        public override string ToString()
        {
            return Name;
        }

    }
}
