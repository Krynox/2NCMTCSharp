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
            FormTicket = new Ticket();
            FormTicketType = new TicketType();
            TicketTypeIndex = 0;
            TypeHeight = 0;
            ZoekHeight = 0;
            AddHeight = 0;
            CloseVis = "Hidden";
            CloseTypeVis = "Hidden";
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
            set { ticketTypeList = value; OnPropertyChanged("TicketTypeList"); }
        }
        #endregion

        #region Selected        
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
        private TicketType _formTicketType;

        public TicketType FormTicketType
        {
            get { return _formTicketType; }
            set { _formTicketType = value; OnPropertyChanged("FormTicketType"); }
        }
        
        #region Heights
        private int _zoekHeight;

        public int ZoekHeight
        {
            get { return _zoekHeight; }
            set { _zoekHeight = value; OnPropertyChanged("ZoekHeight"); }
        }

        private int _addHeigt;

        public int AddHeight
        {
            get { return _addHeigt; }
            set { _addHeigt = value; OnPropertyChanged("AddHeight"); }
        }
        private int _typeHeight;

        public int TypeHeight
        {
            get { return _typeHeight; }
            set { _typeHeight = value; OnPropertyChanged("TypeHeight"); }
        }
        
        #endregion
        #region Visibilty
        private string _closeVis;

        public string CloseVis
        {
            get { return _closeVis; }
            set { _closeVis = value; OnPropertyChanged("CloseVis"); }
        }
        private string _addVis;

        public string AddVis
        {
            get { return _addVis; }
            set { _addVis = value; OnPropertyChanged("AddVis"); }
        }
        private string _editVis;

        public string EditVis
        {
            get { return _editVis; }
            set { _editVis = value; OnPropertyChanged("EditVis"); }
        }
        private string _closeTypeVis;

        public string CloseTypeVis
        {
            get { return _closeTypeVis; }
            set { _closeTypeVis = value; OnPropertyChanged("CloseTypeVis"); }
        }
        private string _addTypeVis;

        public string AddTypeVis
        {
            get { return _addTypeVis; }
            set { _addTypeVis = value; OnPropertyChanged("AddTypeVis"); }
        }
        private string _editTypeVis;

        public string EditTypeVis
        {
            get { return _editTypeVis; }
            set { _editTypeVis = value; OnPropertyChanged("EditTypeVis"); }
        }
        
        #endregion
        #region Commands
        public ICommand AddReserveringClick
        {
            get { return new RelayCommand(OpenAdd); }
        }
        public ICommand ZoekReserveringClick
        {
            get { return new RelayCommand(ZoekReserveringOpen); }
        }
        public ICommand CloseClick
        {
            get { return new RelayCommand(CloseClickAll); }
        }
        public ICommand CloseTypes
        {
            get { return new RelayCommand(CloseType); }
        }
        public ICommand OpenAddType
        {
            get { return new RelayCommand(OAddType); }
        }
        public ICommand OpenEditType
        {
            get { return new RelayCommand(OEditType); }
        }
        public ICommand DeleteType
        {
            get { return new RelayCommand(DType); }
        }
        public ICommand OpslaanNewType
        {
            get { return new RelayCommand(OpslaanType); }
        }
        public ICommand EditNewType
        {
            get { return new RelayCommand(EditType); }
        }
        public ICommand EditReserveringClick
        {
            get { return new RelayCommand(EditOpen); }
        }
        #endregion
        private void CloseClickAll()
        {
            ZoekHeight = 0;
            AddHeight = 0;
            FormTicket = new Ticket();
            CloseVis = "Hidden";
        }

        private void EditOpen()
        {
            if (SelectedTicket != null)
            { 
            CloseClickAll();
            AddHeight = 350;
            EditVis = "Visible";
            AddVis = "Hidden";
            CloseVis = "Visible";
            FormTicket = SelectedTicket;
            int aantal = 0;
            foreach (TicketType c in TicketTypeList)
            {
                if (c.ID == SelectedTicket.TicketType.ID)
                {
                    TicketTypeIndex = aantal;
                }
                aantal++;
            }
            }

        }

        private void OpenAdd()
        {
            CloseClickAll();
            AddHeight = 350;
            EditVis = "Hidden";
            AddVis = "Visible";
            CloseVis = "Visible";
        }

        private void ZoekReserveringOpen()
        {
            CloseClickAll();
            ZoekHeight = 250;
            CloseVis = "Visible";
        }

        #endregion
        #region Ticket
        private void CloseType()
        {
            TypeHeight = 0;
            FormTicketType = new TicketType();
            CloseTypeVis = "Hidden";
            AddTypeVis = "Hidden";
            EditTypeVis = "Hidden";
        }
        private void OAddType()
        {
            CloseType();
            CloseTypeVis = "Visible";
            AddTypeVis = "Visible";
            TypeHeight = 150;
        }

        private void OEditType()
        {
            if (SelectedType != null)
            {
                CloseType();
                CloseTypeVis = "Visible";
                EditTypeVis = "Visible";
                TypeHeight = 150;
                FormTicketType = SelectedType;
            }

        }
        private void DType()
        {
            if(SelectedType!=null)
            {
                 MessageBoxResult result = MessageBox.Show("Wil je " + SelectedType.Name.Trim() + " verwijderen?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    string message = TicketType.DeleteFunctie(SelectedType);
                    if (message != null)
                    {
                        MessageBox.Show(message);
                    }
                    TicketTypeList = TicketType.GetTicketTypes();
                }
            }
        }
        private void OpslaanType()
        {
            TicketType.AddType(FormTicketType);
            TicketTypeList = TicketType.GetTicketTypes();
            CloseType();
        }
        private void EditType()
        {
            string message = TicketType.EditType(FormTicketType);
            if (message != null)
            {
                MessageBox.Show(message);
            }
            else
            {
                CloseType();
                TicketTypeList = TicketType.GetTicketTypes();
            }
        }
        #endregion
        #region Reservering
        public ICommand TicketEditCommand
        {
            get { return new RelayCommand(EditTicket); }
        }

        private void EditTicket()
        {
            if ((SelectedType.AvailableTickets - Convert.ToInt32(TicketType.GetUsedTickets(SelectedType.ID))) >= FormTicket.Amount)
            {
                Ticket.EditTicket(FormTicket);
                TicketList = Ticket.GetTickets();
                TicketTypeList = TicketType.GetTicketTypes();
                CloseClickAll();
            }
            else
            {
                MessageBox.Show("Er zijn niet genoeg tickets vrij van dit type");
            }
        }
        public ICommand TicketAddCommand
        {
            get { return new RelayCommand(AddTicket); }
        }

        private void AddTicket()
        {
            if ((SelectedType.AvailableTickets-Convert.ToInt32(TicketType.GetUsedTickets(SelectedType.ID))) >= FormTicket.Amount)
            {
                Ticket.AddTicket(FormTicket);
                TicketList = Ticket.GetTickets();
                TicketTypeList = TicketType.GetTicketTypes();
                CloseClickAll();
            }
            else {
                MessageBox.Show("Er zijn niet genoeg tickets vrij van dit type");
            }

        }
        public ICommand TicketSearchCommand
        {
            get { return new RelayCommand(TicketSearch); }
        }

        private void TicketSearch()
        {
            TicketList=Ticket.GetTicketsZoek(FormTicket.EntryName,FormTicket.TicketHolder,FormTicket.TicketType);
            CloseClickAll();
        }
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
        #endregion
    }
}
