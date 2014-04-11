using System;
using System.Collections.Generic;
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
        private List<LFile> _Files;
        public List<LFile> Files { get {return syncDown("Files", _Files); } set { _Files = syncUp("Files", value); } }
        [Synchronize]
        private DateTime _SubmitTime;
        public DateTime SubmitTime { get {return syncDown("SubmitTime", _SubmitTime); } set { _SubmitTime = syncUp("SubmitTime", value); } }
        [Synchronize]
        private string _Summary;
        public string Summary { get {return syncDown("Summary", _Summary); } set { _Summary = syncUp("Summary", value); } }
        [Synchronize]
        private Group _SmGroup;
        public Group SmGroup { get {return syncDown("SmGroup", _SmGroup); } set { _SmGroup = syncUp("SmGroup", value); } }
    
    
    
    
    
    
    
    
    }
}
