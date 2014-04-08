using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Group
    {
        public string name { get; set; }
        public Course course { get; set; }
        //public Student leader;
        public List<Student> students { get; set; }
        public override string ToString()
        {
            return name;
        }
    }
}
