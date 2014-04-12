using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iteration3wpf
{
    public class LFile : Loadable<LFile>
    {
        [Synchronize(true)]
        private int _Id;
        public override int Id { get {return syncDown("Id", _Id); } set { _Id = syncUp("Id", value); } }

        [Synchronize(true)]
        private string _FileName;
        public string FileName { get { return syncDown("FileName", _FileName); } set { _FileName = syncUp("FileName", value); } }

        [Synchronize(true)]
        private string _Path;
        public string Path { get {return syncDown("Path", _FileName); } set { _FileName = syncUp("Path", value); } }

        public string relativePath { get { return Path + "\\" + FileName; } }
        protected LFile(int id)
            : base(id)
        {
            _Id = id;
        }
    }
}
