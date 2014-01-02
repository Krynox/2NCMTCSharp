using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
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
            WindowHeight = 600;
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

        #region HeightsProps
        private int _windowsHeight;

        public int WindowHeight
        {
            get { return _windowsHeight; }
            set { _windowsHeight = value; OnPropertyChanged("WindowHeight"); }
        }
        
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

        #region FormWaarden
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
        #endregion

        #region VisibiltyProps
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

        #region CloseReservering
        public ICommand CloseClick
        {
            get { return new RelayCommand(CloseClickAll); }
        }
        private void CloseClickAll()
        {
            ZoekHeight = 0;
            AddHeight = 0;
            int CurrType = TicketTypeIndex;
            FormTicket = new Ticket();
            TicketTypeIndex = CurrType;
            CloseVis = "Hidden";
            WindowHeight = 600;
        }
        #endregion

        #region CloseType
        public ICommand CloseTypes
        {
            get { return new RelayCommand(CloseType); }
        }
        private void CloseType()
        {
            TypeHeight = 0;
            FormTicketType = new TicketType();
            CloseTypeVis = "Hidden";
            AddTypeVis = "Hidden";
            EditTypeVis = "Hidden";
            WindowHeight = 600;
        }
        #endregion

        #region OpenReserveringAdd
        public ICommand AddReserveringClick
        {
            get { return new RelayCommand(OpenAdd); }
        }
        private void OpenAdd()
        {
            int CurrType = TicketTypeIndex;
            CloseClickAll();
            AddHeight = 250;
            EditVis = "Hidden";
            AddVis = "Visible";
            CloseVis = "Visible";
            WindowHeight = 300;
            TicketTypeIndex = CurrType;
        }
        #endregion

        #region OpenReserveringEdit
        public ICommand EditReserveringClick
        {
            get { return new RelayCommand(EditOpen, IsTicketSelected); }
        }
        private void EditOpen()
        {
            if (SelectedTicket != null)
            {
                CloseClickAll();
                AddHeight = 250;
                EditVis = "Visible";
                AddVis = "Hidden";
                CloseVis = "Visible";
                FormTicket = SelectedTicket;
                int aantal = 0;
                WindowHeight = 300;
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
        #endregion

        #region OpenReserveringZoek
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

        #region OpenTypeAdd
        public ICommand OpenAddType
        {
            get { return new RelayCommand(OAddType); }
        }
        private void OAddType()
        {
            CloseType();
            CloseTypeVis = "Visible";
            AddTypeVis = "Visible";
            TypeHeight = 200;
            WindowHeight = 300;
        }
        #endregion

        #region OpenTypeEdit
        public ICommand OpenEditType
        {
            get { return new RelayCommand(OEditType, IsTypeSelected); }
        }
        private void OEditType()
        {
            if (SelectedType != null)
            {
                CloseType();
                CloseTypeVis = "Visible";
                EditTypeVis = "Visible";
                TypeHeight = 200;
                WindowHeight = 300;
                FormTicketType = SelectedType;
            }

        }
        #endregion

        #region DeleteType
        public ICommand DeleteType
        {
            get { return new RelayCommand(DType, IsTypeSelected); }
        }
        private void DType()
        {
            if (SelectedType != null)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Wil je " + SelectedType.Name.Trim() + " verwijderen?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    string message = TicketType.DeleteFunctie(SelectedType);
                    if (message != null)
                    {
                        System.Windows.MessageBox.Show(message);
                    }
                    TicketTypeList = TicketType.GetTicketTypes();
                }
            }
        }
        #endregion

        #region AddType
        public ICommand OpslaanNewType
        {
            get { return new RelayCommand(OpslaanType, IsTypeComplete); }
        }
        private void OpslaanType()
        {
            TicketType.AddType(FormTicketType);
            TicketTypeList = TicketType.GetTicketTypes();
            CloseType();
        }
        #endregion

        #region EditType
        public ICommand EditNewType
        {
            get { return new RelayCommand(EditType, IsTypeComplete); }
        }
        private void EditType()
        {
            string message = TicketType.EditType(FormTicketType);
            if (message != null)
            {
                System.Windows.MessageBox.Show(message);
            }
            else
            {
                CloseType();
                TicketTypeList = TicketType.GetTicketTypes();
            }
        }
        #endregion

        #region EditTicket
        public ICommand TicketEditCommand
        {
            get { return new RelayCommand(EditTicket, IsTicketComplete); }
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
                System.Windows.MessageBox.Show("Er zijn niet genoeg tickets vrij van dit type");
            }
        }
        #endregion

        #region AddTicket
        public ICommand TicketAddCommand
        {
            get { return new RelayCommand(AddTicket, IsTicketComplete); }
        }

        private void AddTicket()
        {
            if ((SelectedType.AvailableTickets - Convert.ToInt32(TicketType.GetUsedTickets(SelectedType.ID))) >= FormTicket.Amount)
            {
                Ticket.AddTicket(FormTicket);
                TicketList = Ticket.GetTickets();
                TicketTypeList = TicketType.GetTicketTypes();
                CloseClickAll();
            }
            else
            {
                System.Windows.MessageBox.Show("Er zijn niet genoeg tickets vrij van dit type");
            }

        }
        #endregion

        #region DeleteTicket
        public ICommand DeleteReserveringClick
        {
            get { return new RelayCommand(DeleteReservering, IsTicketSelected); }
        }
        private void DeleteReservering()
        {
            if (SelectedTicket != null)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Wil je " + SelectedTicket.TicketHolder.Trim() + " verwijderen?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Ticket.DeleteTicket(SelectedTicket);
                    TicketList = Ticket.GetTickets();
                    CloseClickAll();
                }
            }
        }
        #endregion

        #region SearchTicket
        public ICommand TicketSearchCommand
        {
            get { return new RelayCommand(TicketSearch); }
        }

        private void TicketSearch()
        {
            TicketList = Ticket.GetTicketsZoek(FormTicket.EntryName, FormTicket.TicketHolder, FormTicket.TicketType);
            CloseClickAll();
        }
        #endregion

        #region TicketPrint
        public ICommand PrintTicketClick
        {
            get { return new RelayCommand(TicketPrint, IsTicketSelected); }
        }

        private  void TicketPrint()
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            DialogResult r = f.ShowDialog();

            if (r == DialogResult.OK)
            {
                string path = f.SelectedPath;
                Ticket s = new Ticket();
                Print(SelectedTicket, path);
            }
        }
        #endregion

        public static void Print(Ticket SelectedTicket, string path)
        {
            string fileName = SelectedTicket.TicketHolder.Trim() + SelectedTicket.TicketType.Name.Trim() + ".docx";
            string finalPath = path + "\\" + fileName;

            string code = SelectedTicket.TicketHolder + SelectedTicket.ID;

            try
            {
                File.Copy("template.docx", finalPath, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            WordprocessingDocument doc = WordprocessingDocument.Open(finalPath, true);
            IDictionary<string, BookmarkStart> bookmarks = new Dictionary<string, BookmarkStart>();

            foreach (BookmarkStart bms in doc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
            {
                bookmarks[bms.Name] = bms;
            }
            string TicketCode = SelectedTicket.ID + SelectedTicket.TicketHolder[0] + SelectedTicket.TicketHolder[1] + SelectedTicket.TicketHolder[2];
            bookmarks["TicketName"].Parent.InsertAfter<Run>(new Run(new Text("Electronic Rampage")), bookmarks["TicketName"]);
            bookmarks["Name"].Parent.InsertAfter<Run>(new Run(new Text(SelectedTicket.TicketHolder)), bookmarks["Name"]);
            bookmarks["Code"].Parent.InsertAfter<Run>(new Run(new Text(TicketCode)), bookmarks["Code"]);
            bookmarks["TicketType"].Parent.InsertAfter<Run>(new Run(new Text(SelectedTicket.TicketType.Name)), bookmarks["TicketType"]);

            Run run = new Run(new Text(code));
            RunProperties prop = new RunProperties();
            RunFonts font = new RunFonts() { Ascii = "Free 3 of 9 Extended", HighAnsi = "Free 3 of 9 Extended" };
            FontSize size = new FontSize() { Val = SelectedTicket.ID.ToString() };

            prop.Append(font);
            prop.Append(size);
            run.PrependChild<RunProperties>(prop);

            bookmarks["Barcode"].Parent.InsertAfter<Run>(run, bookmarks["Barcode"]);

            doc.Close();
            System.Windows.MessageBox.Show("Ticket is aangemaakt");
        }

        private bool IsTypeSelected()
        {
            if (SelectedType!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsTicketSelected()
        {
            if (SelectedTicket!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //IsValid Werkt niet in deze configuartie dus heb ik een kleine oplossing geschreven (niet volledig er zit geen datavalidatie in)
        private bool IsTypeComplete()
        {
            if (FormTicketType.Name != null && FormTicketType.Price != 0 && FormTicketType.AvailableTickets!=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsTicketComplete()
        {
            if (FormTicket.TicketHolder != null && FormTicket.TicketHolderEmail != null && FormTicket.TicketType != null && FormTicket.Amount!=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
