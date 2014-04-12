using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class PasswordRequest : Loadable<PasswordRequest>
    {
        [Synchronize(true)]
        private int _Id;
        public override int Id { get { return syncDown("Id", _Id); } set { _Id = syncUp("Id", value); } }
        [Synchronize]
        private User _UserRequesting;
        public User UserRequesting { get { return syncDown("UserRequesting", _UserRequesting); } set { _UserRequesting = syncUp("UserRequesting", value); } }
        [Synchronize]
        private bool _AlreadyHandled;
        public bool AlreadyHandled { get { return syncDown("AlreadyHandled", _AlreadyHandled); } set { _AlreadyHandled = syncUp("AlreadyHandled", value); } }


        protected PasswordRequest(int id)
            : base(id)
        {
            _Id = id;
        }

        public override string ToString()
        {
            return UserRequesting != null ? UserRequesting.ToString() : "invalid";
        }
    }
}
