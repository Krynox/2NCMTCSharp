using Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSite.ViewModel
{
    public class TicketVM
    {
        public ObservableCollection<TicketType> types { get; set; }
        public Ticket ticket { get; set; }
        public int SelectedType { get; set; }
        public SelectList typeList { get; set; }
        public string error { get; set; }

        public TicketVM()
        {
            error = "";
            types = TicketType.GetTicketTypes();
            ticket = new Ticket();
            typeList = new SelectList(types, "ID", "Name", SelectedType);
        }
    }
}