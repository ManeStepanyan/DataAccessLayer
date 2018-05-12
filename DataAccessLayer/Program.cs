using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            DAL<Passenger> dal = new DAL<Passenger>();
            ICollection<KeyValuePair<string, object>> collection = new List<KeyValuePair<string, object>>();
            collection.Add(new KeyValuePair<string, object>("ID", 5));
            var x = dal.GetData("GetPassenger", collection);
            foreach (var item in x)
            {
                Console.WriteLine(item.ID + " " + item.Name);
            }
        }
    }
}
