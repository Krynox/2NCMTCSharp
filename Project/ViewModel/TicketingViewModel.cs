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
            
            TicketTypeIndex = 0;

            ZoekHeight = 0;
            CloseVis = "Hidden";
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
        private int _zoekHeight;

        public int ZoekHeight
        {
            get { return _zoekHeight; }
            set { _zoekHeight = value; OnPropertyChanged("ZoekHeight"); }
        }
        
        private int _ticketTypeIndex;

        public int TicketTypeIndex
        {
            get { return _ticketTypeIndex; }
            set { _ticketTypeIndex = value; OnPropertyChanged("TicketTypeIndex"); }
        }
        
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

        private string _closeVis;

        public string CloseVis
        {
            get { return _closeVis; }
            set { _closeVis = value; OnPropertyChanged("CloseVis"); }
        }
        
        private void CloseClickAll()
        {
            ZoekHeight = 0;
            CloseVis = "Hidden";
        }
        public ICommand CloseClick
        {
            get { return new RelayCommand(CloseClickAll); }
        }
        public ICommand ZoekReserveringClick
        {
            get { return new RelayCommand(ZoekReserveringOpen); }
        }

        private void ZoekReserveringOpen()
        {
            CloseClickAll();
            ZoekHeight = 250;
            CloseVis = "Visible";
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
