using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModel
{
    class TicketingViewModel : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Ticketing"; }
        }
        public TicketingViewModel()
        {
            TicketTypeList = TicketType.GetTicketTypes();
            TicketList = Ticket.GetTickets();
        }
        #region Lists
        private ObservableCollection<Ticket> _ticketList;

        public ObservableCollection<Ticket> TicketList
        {
            get { return _ticketList; }
            set { _ticketList = value; OnPropertyChanged("TicketList"); }
        }
        
        private ObservableCollection<TicketType> ticketTypeList;

        public ObservableCollection<TicketType> TicketTypeList
        {
            get { return ticketTypeList; }
            set { ticketTypeList = value; }
        }
        #endregion
        #region Selected
        private Ticket _selctedTicket;

        public Ticket SelectedTicket
        {
            get { return _selctedTicket; }
            set { _selctedTicket = value; OnPropertyChanged("SelectedTicket"); }
        }
        
        private TicketType _selectedType;
        public TicketType SelectedType
        {
            get { return _selectedType; }
            set { _selectedType = value; OnPropertyChanged("SelectedType"); }
        }
    #endregion
        #region Controls
        private Ticket _formTicket;

        public Ticket FormTicket
        {
            get { return _formTicket; }
            set { _formTicket = value; OnPropertyChanged("FormTicket"); }
        }
        
        private void CloseClickAll()
        { 
            
        }
        #endregion
        public ICommand DeleteReserveringClick
        {
            get { return new RelayCommand(DeleteReservering); }
        }
        private void DeleteReservering()
        {
            if (SelectedTicket != null)
            {
                MessageBoxResult result = MessageBox.Show("Wil je " + SelectedTicket.TicketHolder.Trim() + " verwijderen?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Ticket.DeleteTicket(SelectedTicket);
                    TicketList = Ticket.GetTickets();
                    CloseClickAll();
                }
            }
        }
    }
}
