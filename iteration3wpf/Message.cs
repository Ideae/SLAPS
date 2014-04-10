using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Message:Loadable<Message>
    {

        [Synchronize]
        private int _Id;
        public int Id { get { return _Id; } set { _Id = sync("Id", value); } }
        [Synchronize]
        private string _Title;
        public string Title { get { return _Title; } set { _Title = sync("Title", value); } }
        [Synchronize]
        private string _Content;
        public string Content { get { return _Content; } set { _Content = sync("Content", value); } }
        [Synchronize]
        private User _Sender;
        public User Sender { get { return _Sender; } set { _Sender = sync("Sender", value); } }
        [Synchronize]
        private List<Loadable> _Recievers;
        public List<Loadable> Recievers { get { return _Recievers; } set { _Recievers = sync("Recievers", value); } }
        [Synchronize]
        private List<LoadableFile> _Attatchments;
        public List<LoadableFile> Attatchments { get { return _Attatchments; } set { _Attatchments = sync("Attatchments", value); } }


        public override string ToString()
        {
            return name;
        }
    }
}
