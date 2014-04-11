using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<User> _Members = new ObservableCollection<User>();
        public ObservableCollection<User> Members { get {return syncDown("Members", _Members); } set { _Members = syncUp("Members", value); } }
        [Synchronize]
        private Project _GrpProject;
        public Project GrpProject { get { return syncDown("GrpProject", _GrpProject); } set { _GrpProject = syncUp("GrpProject", value); } }
        [Synchronize]
        private Submission _GrpSubmission;
        public Submission GrpSubmission { get { return syncDown("GrpSubmission", _GrpSubmission); } set { _GrpSubmission = syncUp("GrpSubmission", value); } }

        protected Group(int id)
            : base(id)
        {
            _Id = id;
            _Members.CollectionChanged += delegate { Members = _Members; };

        }

        public override string ToString()
        {
            return Name;
        }

    }
}
