using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PassengerInTrip
    {
        public PassengerInTrip() { }
        public PassengerInTrip(int tripNo, DateTime date, int passengerID, string placeToSit)
        {
            TripNo = tripNo;
            Date = date;
            PassengerID = passengerID;
            PlaceToSit = placeToSit;
        }

        [DataName("trip_no")]
        public int TripNo { get; set; }
        [DataName("date")]
        public DateTime Date { get; set; }
        [DataName("ID_psg")]
        public int PassengerID { get; set; }
        [DataName("place")]
        public string PlaceToSit { get; set; }
    }
}
