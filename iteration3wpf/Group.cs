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
        [Synchronize]
        private int _Id;
        public override int Id { get { return _Id; } set { _Id = syncUp("Id", value); } }
        [Synchronize]
        private string _Name;
        public string Name { get { return _Name; } set { _Name = syncUp("Name", value); } }
        [Synchronize]
        private List<User> _Members;
        public List<User> Members { get { return _Members; } set { _Members = syncUp("Members", value); } }
        [Synchronize]
        private Project _GrpProject;
        public Project GrpProject { get { return _GrpProject; } set { _GrpProject = syncUp("Project", value); } }
        [Synchronize]
        private float _Mark;
        public float Mark { get { return _Mark; } set { _Mark = syncUp("Mark", value); } }
        [Synchronize]
        private Submission _GrpSubmission;
        public Submission GrpSubmission { get { return _GrpSubmission; } set { _GrpSubmission = syncUp("Submission", value); } }

        
        public Group() { }
        public override string ToString()
        {
            return Name;
        }

    }
}
