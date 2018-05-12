using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{

    [DataName("Passenger")]
    public class Passenger
    {
        public Passenger()
        {
        }
        public Passenger(int iD, string name)
        {
            ID = iD;
            Name = name;
        }

        [DataName("ID_psg")]
        public int ID { get; set; }
        [DataName("name")]
        public string Name { get; set; }
    }
}
