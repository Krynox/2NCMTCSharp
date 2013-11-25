using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class TicketType
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int AvailableTickets { get; set; }

        public static ObservableCollection<TicketType> GetTicketTypes()
        {
            

            return null;
        }
    }
    
}
