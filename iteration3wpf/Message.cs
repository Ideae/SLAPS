using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Message
    {
        //public File[] attachments;
        public string name;
        public string message;
        public User sender;
        public List<User> recievers;

        public override string ToString()
        {
            return name;
        }
    }
}
