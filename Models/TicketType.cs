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
    public class TicketType : IDataErrorInfo
    {
        public string ID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [Range(1, 1000000)]
        public double Price { get; set; }
        [Required]
        [Range(1, 1000000)]
        public int AvailableTickets { get; set; }
        public string UsedTickets { get; set; }

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
                Name = record["TicketName"].ToString().Trim(),
                Price = Convert.ToDouble(record["Price"].ToString().Trim()),
                AvailableTickets = Convert.ToInt32(record["Available"].ToString().Trim()),
                UsedTickets=(Convert.ToInt32(record["Available"].ToString())-Convert.ToInt32(GetUsedTickets(record["ID"].ToString()))).ToString()+" / "+ record["Available"].ToString()
            };
        }

        public static string GetUsedTickets(string p)
        {
            string used = "";
            DbDataReader reader = Database.GetData("SELECT count(*) as aantal FROM tbl_ticket WHERE TicketType=@id",
                Database.AddParameter("@id",Convert.ToInt32(p))
                );
            foreach (IDataRecord db in reader)
            {
                used = db["aantal"].ToString();
            }
            return used;
        }

        public static string DeleteFunctie(TicketType SelectedType)
        {
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_ticket,tbl_ticketType WHERE tbl_ticketType.ID=tbl_ticket.TicketType AND tbl_ticketType.TicketName=@name", Database.AddParameter("@name", SelectedType.Name));
            if (reader.HasRows)
            {
                return "Gelieve eerste alle reserveringen met dit tickettype te verwijderen";
            }
            else
            {
                Database.ModifyData("DELETE FROM tbl_ticketType WHERE ID=@id", Database.AddParameter("@id", Convert.ToInt32(SelectedType.ID)));
            }
            return null;
        }

        public static string EditType(TicketType FormTicketType)
        {
            if (Convert.ToInt32(GetUsedTickets(FormTicketType.ID.ToString())) <= FormTicketType.AvailableTickets)
            {
                Database.ModifyData("UPDATE tbl_ticketType SET TicketName=@name,Price=@price,Available=@avail WHERE ID=@id",
                        Database.AddParameter("@name",FormTicketType.Name),
                        Database.AddParameter("@price",FormTicketType.Price),
                        Database.AddParameter("@avail",FormTicketType.AvailableTickets),
                        Database.AddParameter("@id",FormTicketType.ID)
                    );
            }
            else
            {
                return "Er zijn niet genoeg tickets vrij";
            }
            return null;
        }
        public static void AddType(TicketType FormTicketType)
        {
            Database.ModifyData("INSERT INTO tbl_ticketType (TicketName,Price,Available) VALUES(@name,@price,@avail)",
                Database.AddParameter("@name", FormTicketType.Name),
                Database.AddParameter("@price", FormTicketType.Price),
                Database.AddParameter("@avail", FormTicketType.AvailableTickets)
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
