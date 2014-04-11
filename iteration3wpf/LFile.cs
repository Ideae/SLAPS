using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iteration3wpf
{
    public class LFile : Loadable<LFile>
    {
        [Synchronize]
        private int _Id;
        public override int Id { get { return _Id; } set { _Id = syncUp("Id", value); } }

        [Synchronize]
        private string _Path;
        public string Path { get { return _Path; } set { _Path = syncUp("Path", value); } }
    }
}
