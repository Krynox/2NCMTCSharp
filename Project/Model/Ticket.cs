using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Ticket
    {
        public string ID { get; set; }
        public string TicketHolder { get; set; }
        public string TicketHolderEmail { get; set; }
        public TicketType TicketType { get; set; }
        public int Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public string EntryName { get; set; }

        public static ObservableCollection<Ticket> GetTickets()
        {
            ObservableCollection<Ticket> tickets = new ObservableCollection<Ticket>();
            DbDataReader reader = Database.GetData("SELECT tbl_ticket.ID as TicketId,tbl_ticketType.ID as TypeId ,Ticketholder,TicketholderEmail, EntryName, Amount, OrderDate,TicketName,Price,Available FROM tbl_ticketType,tbl_ticket WHERE tbl_ticketType.ID=tbl_ticket.TicketType");
            foreach (IDataRecord db in reader)
            {
                tickets.Add(Create(db));
            }
            return tickets;

        }
        private static Ticket Create(IDataRecord record)
        {
            return new Ticket()
            {
                ID = record["TicketId"].ToString(),
                TicketHolder = record["Ticketholder"].ToString(),
                TicketHolderEmail = record["TicketholderEmail"].ToString(),
                TicketType = new TicketType(){ ID=record["TypeId"].ToString(),Name=record["TicketName"].ToString(), Price=Convert.ToDouble(record["Price"]),AvailableTickets=Convert.ToInt32(record["Available"]) },
                Amount = Convert.ToInt32(record["Amount"].ToString()),
                EntryName=record["EntryName"].ToString(),
                OrderDate=Convert.ToDateTime(record["OrderDate"]),

            };
        }
        public static void DeleteTicket(Ticket SelectedTicket)
        {
            Database.ModifyData("DELETE FROM tbl_ticket WHERE ID=@id", Database.AddParameter("@id", SelectedTicket.ID));
        }
    }
}
