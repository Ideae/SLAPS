using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public abstract class User
    {
        // attributes //
        public string fullname { get; set; }
        public string username { get; set; } // stores the username of the user
        public string password { get; set; } // stores the password of the user
        public int ID;

        // methods //
        public void changePassword() { } // method to change the user’s password
        public void login() { } // checks the user’s credentials upon login
        public void logout() { } // method to log the user out

        public void getSLAPs() { }
        public void sendSLAPs() { }
        public override string ToString()
        {
            return username;
        }
    }
    


}
    
    
