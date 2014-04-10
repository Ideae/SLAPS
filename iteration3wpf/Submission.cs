using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Submission : Loadable<Submission>
    {
        [Synchronize]
        private int _Id;
        public int Id { get { return _Id; } set { _Id = sync("Id", value); } }
        [Synchronize]
        private Project _SmProject;
        public Project SmProject { get { return _SmProject; } set { _SmProject = sync("SmProject", value); } }
        [Synchronize]
        private User _Submitter;
        public User Submitter { get { return _Submitter; } set { _Submitter = sync("Submitter", value); } }
        [Synchronize]
        private List<LFile> _Files;
        public List<LFile> Files { get { return _Files; } set { _Files = sync("Files", value); } }
        [Synchronize]
        private DateTime _SubmitTime;
        public DateTime SubmitTime { get { return _SubmitTime; } set { _SubmitTime = sync("SubmitTime", value); } }
        [Synchronize]
        private string _Summary;
        public string Summary { get { return _Summary; } set { _Summary = sync("Summary", value); } }
        [Synchronize]
        private Group _SmGroup;
        public Group SmGroup { get { return _SmGroup; } set { _SmGroup = sync("SmGroup", value); } }
    
    
    
    
    
    
    
    
    }
}
