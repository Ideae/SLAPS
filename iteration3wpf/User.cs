using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public enum usertype{ Student, Admin, Instructor};
    public sealed class User : Loadable<User>
    {


        public const int minPasswordLength = 6;
        // attributes //

        [Synchronize]
        private int _Id;
        public override int Id { get{return _Id;} set{} }
        [Synchronize]
        private string _Username; 
        public string Username { get { return _Username; } set { _Username = syncUp("Username", value); } } // stores the username of the user
        [Synchronize]
        private string _Password;
        public string Password { get { return _Password; } set { _Password = syncUp("Password", value); } } // stores the password of the user
        [Synchronize]
        private string _FirstName;
        public string FirstName { get { return _FirstName; } set { _FirstName = syncUp("FirstName", value); } }
        [Synchronize]
        private string _LastName;
        public string LastName { get { return _LastName; } set { _LastName = syncUp("LastName", value); } }
        [Synchronize]
        private usertype _UserType;
        public usertype UserType { get { return _UserType; } set { _UserType = syncUp("UserType", value); } }
        [Synchronize]
        private List<Course> _Courses;
        public List<Course> Courses { get { return _Courses; } set { _Courses = syncUp("Courses", value); } }
        [Synchronize]
        private List<Group> _Groups;
        public List<Group> Groups { get { return _Groups; } set { _Groups = syncUp("Groups", value); } }
        [Synchronize]
        private List<Submission> _Submissions;
        public List<Submission> Submissions { get { return _Submissions; } set { _Submissions = syncUp("Submissions", value); } }


        public User() { }
        public User(DataRow data) { SetData(data); }

        public static User login(string username, string password)
        {
            DataTable t = SQLiteDB.main.GetDataTable("SELECT * FROM 'Users' WHERE Username=@param1;", username);
            if (t.Rows.Count < 1) return null;
            else if (t.Rows.Count > 1) throw new SystemException("There were two users with the same name in the database");
            else
            {
                DataRow UserRow = t.Rows[0];
                
                Console.WriteLine(UserRow.Field<string>("Password"));
                if (UserRow.Field<string>("Password") != password) return null;
                else return new User(UserRow);
            }
        }
        public void changePassword() { } // method to change the user’s password

        public void getSLAPs() { }
        public void sendSLAPs() { }
        //Utils
        public override string ToString()
        {
            return Username;
        }

    }
    


}
    
    
