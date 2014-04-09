using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Group
    {
        private static List<Group> loadedGroups;
        private System.Data.DataRow dataRow;

        public Group(DataRow dataRow)
        {


        }
        public string name { get; set; }
        public int id { get; set; }
        public Course course { get; set; }
        //public Student leader;
        public List<Student> students { get; set; }
        public override string ToString()
        {
            return name;
        }

        internal static Group getById(int id)
        {
            if (loadedGroups.Any((g)=>g.id == id)) return loadedGroups.First((g)=>g.id == id);
            else return new Group(SQLiteDB.main.getRowById("Groups", id));
        }
    }
}
