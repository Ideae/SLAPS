using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public static class Utilities
    {
        public static string LastWord(this string s, char delim)
        {
            return s.Substring(s.LastIndexOf(delim) + 1);
        }

        public static string TypeName(this object o)
        {
            return o.GetType().ToString().LastWord('.');
        }
    }
}
