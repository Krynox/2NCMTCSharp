using Project;
using ProjectSite.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace ProjectSite.Controllers
{
    public class TicketController : Controller
    {
        //
        // GET: /Ticket/
        [Authorize]
        public ActionResult Index()
        {
            return View(Ticket.GetUserTicket(WebSecurity.CurrentUserName));
        }
        [Authorize]
        public ActionResult BestelTicket()
        {
            TicketVM ticket = new TicketVM();
            return View(ticket);
        }
        public ActionResult Admin()
        {
            return View("Index", Ticket.GetTickets());
        }
        [Authorize]
        [HttpPost]
        public ActionResult BestelTicket(TicketVM T)
        {
            if(T.ticket.TicketHolder!="" && T.ticket.TicketHolderEmail!="" && T.ticket.Amount!=0)
            {
                foreach (TicketType ov in T.types)
                {
                    if (ov.ID == T.SelectedType.ToString())
                    {
                        T.ticket.TicketType = ov;
                    }
                }
                if ((T.ticket.TicketType.AvailableTickets - Convert.ToInt32(TicketType.GetUsedTickets(T.ticket.TicketType.ID))) >= T.ticket.Amount)
                {
                    T.ticket.OrderDate = DateTime.Now;
                    T.ticket.EntryName = WebSecurity.CurrentUserName;
                    Ticket.AddTicket(T.ticket);
                    return RedirectToAction("Index");
                }
                else {
                    T.error = "Er zijn niet genoeg tickets meer  vrij";
                    return View(T);
                }
            }
            return null;
        }
    }
}
