using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{

    [DataName("Company")]
    public class Company
    {
        public Company()
        {
        }
        public Company(int iD, string name)
        {
            ID = iD;
            Name = name;
        }

        [DataName("ID_comp")]
        public int ID { get; set; }
        [DataName("name")]
        public String Name { get; set; }
    }
}
