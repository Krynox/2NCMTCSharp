using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project
{
    public class Ticket : IDataErrorInfo
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "De naam is verplicht")]
        [StringLength(50, MinimumLength = 3,ErrorMessage="Moet langer zijn dan 3 tekens maar niet groter dan 50")]
        public string TicketHolder { get; set; }
        [Required(ErrorMessage = "Het email is verplicht")]
        [StringLength(50, MinimumLength = 3,ErrorMessage="Moet langer zijn dan 3 tekens maar niet groter dan 50")]
        public string TicketHolderEmail { get; set; }
        [Required (ErrorMessage="Je moet een type aanduiden")]
        public TicketType TicketType { get; set; }
        [Required(ErrorMessage = "Een aantal is verplicht")]
        [Range(1, 1000000,ErrorMessage="Je moet minimum 1 ticket bestellen")]
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
                ID = record["TicketId"].ToString().Trim(),
                TicketHolder = record["Ticketholder"].ToString().Trim(),
                TicketHolderEmail = record["TicketholderEmail"].ToString().Trim(),
                TicketType = new TicketType(){ ID=record["TypeId"].ToString(),Name=record["TicketName"].ToString(), Price=Convert.ToDouble(record["Price"]),AvailableTickets=Convert.ToInt32(record["Available"]) },
                Amount = Convert.ToInt32(record["Amount"].ToString().Trim()),
                EntryName = record["EntryName"].ToString().Trim(),
                OrderDate = Convert.ToDateTime(record["OrderDate"])

            };
        }
        public static void DeleteTicket(Ticket SelectedTicket)
        {
            Database.ModifyData("DELETE FROM tbl_ticket WHERE ID=@id", Database.AddParameter("@id", SelectedTicket.ID));
        }
        public static ObservableCollection<Ticket> GetUserTicket(string name)
        {
            ObservableCollection<Ticket> tickets = new ObservableCollection<Ticket>();
            DbDataReader reader = Database.GetData("SELECT tbl_ticket.ID as TicketId,tbl_ticketType.ID as TypeId ,Ticketholder,TicketholderEmail, EntryName, Amount, OrderDate,TicketName,Price,Available FROM tbl_ticketType,tbl_ticket WHERE tbl_ticketType.ID=tbl_ticket.TicketType AND EntryName=@name"
                ,Database.AddParameter("@name",name)
                );
            foreach (IDataRecord db in reader)
            {
                tickets.Add(Create(db));
            }
            reader.Close();
            return tickets;
        }
        public static ObservableCollection<Ticket> GetTicketsZoek(string EntryName, string Name, Project.TicketType ticketType)
        {
            string ticket="";
            if (ticketType == null)
            {
                ticket = "";
            }
            else
            {
                ticket = ticketType.Name;
            }
            ObservableCollection<Ticket> tickets = new ObservableCollection<Ticket>();
            DbDataReader reader = Database.GetData("SELECT tbl_ticket.ID as TicketId,tbl_ticketType.ID as TypeId ,Ticketholder,TicketholderEmail, EntryName, Amount, OrderDate,TicketName,Price,Available FROM tbl_ticketType,tbl_ticket WHERE tbl_ticketType.ID=tbl_ticket.TicketType AND EntryName LIKE @entryname AND TicketHolder LIKE @name AND Tbl_ticketType.TicketName LIKE @tickettype",
                Database.AddParameter("@entryname","%"+EntryName+"%"),
                Database.AddParameter("@name", "%" + Name + "%"),
                Database.AddParameter("@tickettype", "%" + ticket + "%")
                );
            foreach (IDataRecord db in reader)
            {
                tickets.Add(Create(db));
            }
            return tickets;
        }

        public static void AddTicket(Ticket FormTicket)
        {
            if (FormTicket.EntryName == "" || FormTicket.EntryName == null)
            {
                FormTicket.EntryName = "Admin";
            }
            Database.ModifyData("INSERT INTO tbl_ticket (TicketHolder,TicketHolderEmail,TicketType,EntryName,Amount,OrderDate) VALUES (@holder,@holderemail,@type,@entryname,@amount,@date)",
                Database.AddParameter("@holder",FormTicket.TicketHolder),
                Database.AddParameter("@holderemail",FormTicket.TicketHolderEmail),
                Database.AddParameter("@type",FormTicket.TicketType.ID),
                Database.AddParameter("@entryname",FormTicket.EntryName),
                Database.AddParameter("@amount",FormTicket.Amount),
                Database.AddParameter("@date",DateTime.Now)
                );
        }

        public static void EditTicket(Ticket FormTicket)
        {
            Database.ModifyData("UPDATE tbl_ticket SET TicketHolder=@holder,TicketHolderEmail=@holderemail,TicketType=@type,Amount=@amount WHERE ID=@id",
                Database.AddParameter("@holder",FormTicket.TicketHolder),
                Database.AddParameter("@holderemail",FormTicket.TicketHolderEmail),
                Database.AddParameter("@type",FormTicket.TicketType.ID),
                Database.AddParameter("@amount",FormTicket.Amount),
                Database.AddParameter("@id",FormTicket.ID)
                );
        }
        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
        public string Error
        {
            get { return null; }
        }
        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = columnName });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }
    }
}
