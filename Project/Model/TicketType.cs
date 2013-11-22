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
            ObservableCollection<TicketType> contacts = new ObservableCollection<TicketType>();
            StreamReader sr = new StreamReader("TicketType.csv");

            string lijn = "";
            while ((lijn = sr.ReadLine()) != null)
            {
                string[] strLijn = lijn.Split(';');
                TicketType ct = new TicketType();
                ct.ID = strLijn[0];
                ct.Name = strLijn[1];
                ct.Price = Convert.ToDouble(strLijn[2]);
                ct.AvailableTickets = Convert.ToInt32(strLijn[3]);
                contacts.Add(ct);
            }
            sr.Close();

            return contacts;
        }
    }
    
}
