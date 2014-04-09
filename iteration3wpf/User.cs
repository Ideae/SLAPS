using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class User : Loadable<User>
    {

        // attributes //
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<Group> groups { get; set; }
        public List<Submission> submissions { get; set; }
        public List<Course> courses { get; set; }


        public string username { get; set; } // stores the username of the user
        public string password { get; set; } // stores the password of the user
        public int ID;
        protected override void SetData(DataRow UserRow)
        {
            
            ID = UserRow.Field<int>("Id");
            username = UserRow.Field<string>("Username");
            password = UserRow.Field<string>("Password");
            firstName = UserRow.Field<string>("FirstName");
            lastName = UserRow.Field<string>("LastName");

            loadedCache[ID] = this;

            int[] groupList = SQLiteDB.decodeList(UserRow.Field<string>("Groups"));
            int[] courseList = SQLiteDB.decodeList( UserRow.Field<string>("Courses"));
            int[] submissionList = SQLiteDB.decodeList( UserRow.Field<string>("Submissions"));

            foreach (int i in groupList) groups.Add(Group.getById(i));
            foreach (int i in courseList) courses.Add(Course.getById(i));
            foreach (int i in submissionList) submissions.Add(Submission.getById(i));
        }

        public User() { }
        // methods //
        // checks the databse for a user with the specified credentials
        public static User login(string username, string password)
        {
            DataTable t = SQLiteDB.main.GetDataTable("SELECT * FROM Users WHERE Username='@param1';", username);
            if (t.Rows.Count < 1) return null;
            else if (t.Rows.Count > 1) throw new SystemException("There were two users with the same name in the database");
            else
            {
                DataRow UserRow = t.Rows[0];
                if (UserRow.Field<string>("Password") != password) return null;
                else switch (UserRow.Field<string>("UserType"))
                    {
                        case "Admin": return new Administrator(UserRow);
                        case "Instructor": return new Instructor(UserRow);
                        case "Student": return new Student(UserRow);
                        default: throw new SystemException("Invalid Usertype read from DataBase");
                    }
            }
        }
        public void changePassword() { } // method to change the user’s password

        public void logout() { } // method to log the user out

        public void getSLAPs() { }
        public void sendSLAPs() { }
        public override string ToString()
        {
            return username;
        }
    }
    


}
    
    
