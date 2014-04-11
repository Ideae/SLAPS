using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Submission : Loadable<Submission>
    {
        [Synchronize(true)]
        private int _Id;
        public override int Id { get {return syncDown("Id", _Id); } set { _Id = syncUp("Id", value); } }
        [Synchronize(true)]
        private Project _SmProject;
        public Project SmProject { get {return syncDown("SmProject", _SmProject); } set { _SmProject = syncUp("SmProject", value); } }
        [Synchronize]
        private User _Submitter;
        public User Submitter { get {return syncDown("Submitter", _Submitter); } set { _Submitter = syncUp("Submitter", value); } }
        [Synchronize]
        private ObservableCollection<LFile> _Files = new ObservableCollection<LFile>();
        public ObservableCollection<LFile> Files { get {return syncDown("Files", _Files); } set { _Files = syncUp("Files", value); } }
        [Synchronize]
        private DateTime _SubmitTime;
        public DateTime SubmitTime { get {return syncDown("SubmitTime", _SubmitTime); } set { _SubmitTime = syncUp("SubmitTime", value); } }
        [Synchronize]
        private Group _SmGroup;
        public Group SmGroup { get {return syncDown("SmGroup", _SmGroup); } set { _SmGroup = syncUp("SmGroup", value); } }
        [Synchronize]
        private int _Mark;
        public int Mark { get { return syncDown("Mark", _Mark); } set { _Mark = syncUp("Mark", value); } }
        [Synchronize]
        private string _InstructorComments;
        public string InstructorComments { get { return syncDown("InstructorComments", _InstructorComments); } set { _InstructorComments = syncUp("InstructorComments", value); } }

        protected Submission(int id)
            : base(id)
        {
            _Id = id;
            _Files.CollectionChanged += delegate { Files = _Files; }; ;

        }
    
    
    
    
    
    
    }
}
