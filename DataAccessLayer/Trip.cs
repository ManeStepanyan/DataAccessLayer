using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    [DataName("Trip")]
    public class Trip
    {
        public Trip() { }
        public Trip(int tripNo, int companyID, string planeName, string town_from, string town_to, DateTime time_out, DateTime time_in)
        {
            TripNo = tripNo;
            CompanyID = companyID;
            PlaneName = planeName;
            Town_from = town_from;
            Town_to = town_to;
            Time_out = time_out;
            Time_in = time_in;
        }

        [DataName("trip_no")]
        public int TripNo { get; set; }
        [DataName("ID_comp")]
        public int CompanyID { get; set; }
        [DataName("plane")]
        public string PlaneName { get; set; }
        [DataName("town_from")]
        public string Town_from { get; set; }
        [DataName("town_to")]
        public string Town_to { get; set; }
        [DataName("time_out")]
        public DateTime Time_out { get; set; }
        [DataName("time_in")]
        public DateTime Time_in { get; set; }
    }
}
