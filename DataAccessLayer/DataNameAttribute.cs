using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{ // I use this class for mapping class and property names to appropriate name of table,column etc.
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class DataNameAttribute : Attribute
    {
        private String value;
        public string Value { get { return value; } set { this.value = value; } }
        public DataNameAttribute(string s)
        {
            value = s;
        }
    }
}
