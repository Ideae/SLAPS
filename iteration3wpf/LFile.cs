using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iteration3wpf
{
    class LFile : Loadable<LFile>
    {
        [Synchronize]
        private int _Id;
        public int Id { get { return _Id; } set { _Id = sync("Id", value); } }

        [Synchronize]
        private string _Path;
        public string Path { get { return _Path; } set { _Path = sync("Path", value); } }
    }
}
