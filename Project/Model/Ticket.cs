using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Ticket
    {
        public string ID { get; set; }
        public string TicketHolder { get; set; }
        public string TicketHolderEmail { get; set; }
        public TicketType TicketType { get; set; }
        public int Amount { get; set; }
    }
}
