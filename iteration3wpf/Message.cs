using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Message:Loadable<Message>
    {

        [Synchronize(true)]
        private int _Id;
        public override int Id { get {return syncDown("Id", _Id); } set { _Id = syncUp("Id", value); } }
        [Synchronize(true)]
        private string _Title;
        public string Title { get {return syncDown("Title", _Title); } set { _Title = syncUp("Title", value); } }
        [Synchronize]
        private string _Content;
        public string Content { get {return syncDown("Content", _Content); } set { _Content = syncUp("Content", value); } }
        [Synchronize]
        private User _Sender;
        public User Sender { get {return syncDown("Sender", _Sender); } set { _Sender = syncUp("Sender", value); } }
        [Synchronize]
        private List<Loadable> _Recievers;
        public List<Loadable> Recievers { get {return syncDown("Recievers", _Recievers); } set { _Recievers = syncUp("Recievers", value); } }
        [Synchronize]
        private List<LFile> _Attatchments;
        public List<LFile> Attatchments { get {return syncDown("Attatchments", _Attatchments); } set { _Attatchments = syncUp("Attatchments", value); } }


        public override string ToString()
        {
            return Title;
        }
    }
}
