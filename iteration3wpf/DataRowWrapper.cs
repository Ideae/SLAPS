using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace iteration3wpf
{
    public class DataRowWrapper
    {
        public string type;
        public DataRow dataRow;
        public DataRowWrapper(DataRow dr, string type)
        {
            this.type = type;
            dataRow = dr;
        }
        public override string ToString()
        {
            return dataRow.Field<string>(1);
        }

        public int ID
        {
            get { return (int)dataRow.Field<long>(0); }
        }
    }
}
