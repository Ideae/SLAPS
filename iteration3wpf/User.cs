using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public enum usertype{ Student, Admin, Instructor };
    public sealed class User : Loadable<User>
    {
        

        public const int minPasswordLength = 6;
        // attributes //

        [Synchronize(true)]
        private int _Id;
        public override int Id { get{return _Id;} set{} }
        [Synchronize(true)]
        private string _Username; 
        public string Username { get {return syncDown("Username", _Username); } set { _Username = syncUp("Username", value); } } // stores the username of the user
        [Synchronize]
        private string _Password;
        public string Password { get {return syncDown("Password", _Password); } set { _Password = syncUp("Password", value); } } // stores the password of the user
        [Synchronize]
        private string _FirstName;
        public string FirstName { get {return syncDown("FirstName", _FirstName); } set { _FirstName = syncUp("FirstName", value); } }
        [Synchronize]
        private string _LastName;
        public string LastName { get {return syncDown("LastName", _LastName); } set { _LastName = syncUp("LastName", value); } }
        [Synchronize]
        private usertype _UserType;
        public usertype UserType { get {return syncDown("UserType", _UserType); } set { _UserType = syncUp("UserType", value); } }
        [Synchronize]
        private ObservableCollection<Course> _Courses = new ObservableCollection<Course>();
        public ObservableCollection<Course> Courses { get {
            return syncDown("Courses", _Courses);
        } set { _Courses = syncUp("Courses", value); } }
        [Synchronize]
        private ObservableCollection<Group> _Groups = new ObservableCollection<Group>();
        public ObservableCollection<Group> Groups { get {return syncDown("Groups", _Groups); } set { _Groups = syncUp("Groups", value); } }
        [Synchronize]
        private ObservableCollection<Submission> _Submissions = new ObservableCollection<Submission>();
        public ObservableCollection<Submission> Submissions { get {return syncDown("Submissions", _Submissions); } set { _Submissions = syncUp("Submissions", value); } }


        private User(int id)
            : base(id)
        {
            _Id = id;
            _Courses.CollectionChanged += delegate {
                Courses = _Courses; 
            };
            _Groups.CollectionChanged += delegate { Groups = _Groups; };
            _Submissions.CollectionChanged += delegate { Submissions = _Submissions; };
        }

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
                else return User.GetById((int)UserRow.Field<long>("Id"));
            }
        }
        public void changePassword() { } // method to change the user’s password

        public void getSLAPs() { }
        public void sendSLAPs() { }
        //Utils
        public override string ToString()
        {
            if (MainWindow.mainWindow.activeUser.UserType == usertype.Admin) return Username;
            else return FirstName +" "+ LastName;
        }

    }
    


}
    
    
