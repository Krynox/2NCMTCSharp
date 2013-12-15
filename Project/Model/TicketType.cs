using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
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
            ObservableCollection<TicketType> type = new ObservableCollection<TicketType>();
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_ticketType");
            foreach (IDataRecord db in reader)
            {
                type.Add(Create(db));
            }
            return type;

        }

        private static TicketType Create(IDataRecord record)
        {
            return new TicketType()
            {
                ID = record["ID"].ToString(),
                Name = record["TicketName"].ToString(),
                Price = Convert.ToDouble(record["Price"].ToString()),
                AvailableTickets = Convert.ToInt32(record["Available"].ToString()),
            };
        }
    }
    
}
