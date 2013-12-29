﻿using Project.Model;
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
            Database.ModifyData("INSERT INTO tbl_ticket (TicketHolder,TicketHolderEmail,TicketType,EntryName,Amount,OrderDate) VALUES (@holder,@holderemail,@type,@entryname,@amount,@date)",
                Database.AddParameter("@holder",FormTicket.TicketHolder),
                Database.AddParameter("@holderemail",FormTicket.TicketHolderEmail),
                Database.AddParameter("@type",FormTicket.TicketType.ID),
                Database.AddParameter("@entryname","Admin"),
                Database.AddParameter("@amount",FormTicket.Amount),
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  