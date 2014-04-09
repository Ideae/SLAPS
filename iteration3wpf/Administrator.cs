using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Administrator : User
    {
        private System.Data.DataRow UserRow;

        public Administrator(DataRow UserRow)
        {
            SetData(UserRow);
        }
        //List<User> filterUsers(List<User> users, string regEx, OrderEnum order);
    }
}
