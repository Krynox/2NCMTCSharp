using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Project.View;


namespace Project.ViewModel
{
    class ContactPersonenViewModel : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Contactpersonen"; }
        }
        public ContactPersonenViewModel()
        {
            _formContact = new Contactperson();
            ContactList = Contactperson.GetContactPersons();
            TypeList = ContactpersonType.GetTypes();
            AddControls = "Visible";
            EditControls = "Hidden";
            CloseVis = "Hidden";
        }

        #region Lists

        private ObservableCollection<ContactpersonType> _typeList;
        public ObservableCollection<ContactpersonType> TypeList
        {
            get { return _typeList; }
            set { _typeList = value; OnPropertyChanged("TypeList"); }
        }
        private ObservableCollection<Contactperson> contactList;
        public ObservableCollection<Contactperson> ContactList
        {
            get { return contactList; }
            set { contactList = value; OnPropertyChanged("ContactList"); }
        }
        
        #endregion

        #region Selected
        private Contactperson _selectedContact;

        public Contactperson SelectedContact
        {
            get { return _selectedContact; }
            set { _selectedContact = value; OnPropertyChanged("SelectedContact"); }
        }
        private ContactpersonType _selectedType;

        public ContactpersonType SelectedType
        {
            get { return _selectedType; }
            set { _selectedType = value; OnPropertyChanged("SelectedType"); }
        }
        #endregion

        #region FormWaarden
        private Contactperson _formContact;

        public Contactperson FormContact
        {
            get { return _formContact; }
            set { _formContact = value;  OnPropertyChanged("FormContact");}
        }
        
        private string _functieTypeAddText;

        public string FunctieTypeAddText
        {
            get { return _functieTypeAddText; }
            set { _functieTypeAddText = value; OnPropertyChanged("FunctieTypeAddText"); }
        }
        
        private string _contactTypeAdd;

        public string ContactTypeAdd
        {
            get { return _contactTypeAdd; }
            set { _contactTypeAdd = value; OnPropertyChanged("ContactTypeAdd"); }
        }
        #endregion

        #region Controls

        #region Visibility
        private string _closeVis;
        public string CloseVis
        {
            get { return _closeVis; }
            set { _closeVis = value; OnPropertyChanged("CloseVis"); }
        }
        private string _editTypeKnop;

        public string EditTypeKnop
        {
            get { return _editTypeKnop; }
            set { _editTypeKnop = value; OnPropertyChanged("EditTypeKnop"); }
        }
        private string _addTypeKnop;

        public string AddTypeKnop
        {
            get { return _addTypeKnop; }
            set { _addTypeKnop = value; OnPropertyChanged("AddTypeKnop"); }
        }

        private string _addTypeVis;

        public string AddTypeVis
        {
            get { return _addTypeVis; }
            set { _addTypeVis = value; OnPropertyChanged("AddTypeVis"); }
        }
        private string _addType;

        public string AddType
        {
            get { return _addType; }
            set { _addType = value; OnPropertyChanged("AddType"); }
        }
        private string _addControls;

        public string AddControls
        {
            get { return _addControls; }
            set { _addControls = value; OnPropertyChanged("AddControls"); }
        }
        private string _editControls;

        public string EditControls
        {
            get { return _editControls; }
            set { _editControls = value; OnPropertyChanged("EditControls"); }
        }
        #endregion

        #region Height
        private int _functieMenuHeight;

        public int FunctieMenuHeight
        {
            get { return _functieMenuHeight; }
            set { _functieMenuHeight = value; OnPropertyChanged("FunctieMenuHeight"); }
        }

        private int _listIndex;

        public int ListIndex
        {
            get { return _listIndex; }
            set { _listIndex = value; OnPropertyChanged("ListIndex"); }
        }
        private int _menuHeightAdd;
        public int MenuHeightAdd
        {
            get { return _menuHeightAdd; }
            set { _menuHeightAdd = value; OnPropertyChanged("MenuHeightAdd"); }
        }
        private int _menuZoekHeight;
        public int MenuZoekHeight
        {
            get { return _menuZoekHeight; }
            set { _menuZoekHeight = value; OnPropertyChanged("MenuZoekHeight"); }
        }
        #endregion

        #region Commands
        public ICommand FunctieClick
        {
            get
            { return new RelayCommand(OpenFunctie); }
        }
        public ICommand AddTypeClick
        {
            get
            { return new RelayCommand(AddTypeControl); }
        }
        public ICommand EditTypeClick
        {
            get
            { return new RelayCommand(EditType); }
        }
        public ICommand EditTypeClickOpslaan
        {
            get
            { return new RelayCommand(EditTypeOpslaan); }
        }
        public ICommand AddTypeClickOpslaan
        {
            get
            { return new RelayCommand(AddTypeOpslaan); }
        }
        public ICommand CloseClick
        {
            get
            { return new RelayCommand(CloseClickAll); }
        }
        public ICommand TypeAddCommand
        {
            get
            { return new RelayCommand(TypeAddShow); }
        }
        public ICommand AddContactType
        {
            get
            { return new RelayCommand(ContactTypeToevoegen); }
        }
        public ICommand AddMenuClick
        {
            get
            { return new RelayCommand(AddMenu); }
        }
        public ICommand EditMenuClick
        {
            get { return new RelayCommand(EditMenu); }
        }
        public ICommand ZoekClick
        {
            get { return new RelayCommand(Zoek); }
        }
        #endregion

        private void CloseClickAll()
        {
            MenuHeightAdd = 0;
            MenuZoekHeight = 0;
            FunctieMenuHeight = 0;
            CloseVis = "Hidden";
            FormContact = new Contactperson();
            ListIndex = 0;
        }

        private void AddTypeOpslaan()
        {
            ContactpersonType.AddContact(FunctieTypeAddText);
            AddTypeVis = "Hidden";
            TypeList = ContactpersonType.GetTypes();
        }

        private void EditTypeOpslaan()
        {
            ContactpersonType.EditType(SelectedType, FunctieTypeAddText);
            AddTypeVis = "Hidden";
            TypeList = ContactpersonType.GetTypes();
        }

        private void EditType()
        {
            if (SelectedType != null)
            {
                AddTypeVis = "Visible";
                EditTypeKnop = "Visible";
                AddTypeKnop = "Hidden";
                FunctieTypeAddText = SelectedType.Name.Trim();
            }
        }

        private void AddTypeControl()
        {
            AddTypeVis = "Visible";
            EditTypeKnop = "Hidden";
            AddTypeKnop = "Visible";
            FunctieTypeAddText = "";
        }
      
        private void OpenFunctie()
        {
            CloseClickAll();
            FunctieMenuHeight = 300;
            CloseVis = "Visible";
            AddTypeVis = "Hidden";
        
        }
       
        private void TypeAddShow()
        {
            AddType = "Visible";
        }
      
        private void ContactTypeToevoegen()
        {
            ContactpersonType.AddContact(ContactTypeAdd);
            AddType = "Hidden";
            TypeList=ContactpersonType.GetTypes();
        }
       
        private void AddMenu()
        {
            CloseClickAll();
            MenuHeightAdd = 400;
            AddControls = "Visible";
            EditControls = "Hidden";
            CloseVis = "Visible";
            AddType = "Hidden";

        }

        private void EditMenu()
        {
            if (SelectedContact != null)
            {
                CloseClickAll();
                MenuHeightAdd = 400;
                AddControls = "Hidden";
                EditControls = "Visible";
                CloseVis = "Visible";
                AddType = "Hidden";
                FormContact = SelectedContact;
                int aantal = 0;
                foreach (ContactpersonType c in TypeList)
                {
                    if (c.ID == SelectedContact.JobRole.ID)
                    {
                        ListIndex = aantal;
                    }
                    aantal++;
                }
            }
        }

        private void Zoek()
        {
            CloseClickAll();
            MenuZoekHeight = 250;
            CloseVis = "Visible";
        }

        #endregion

        public ICommand VerwijderTypeClick
        {
            get
            { return new RelayCommand(VerwijderType); }
        }
        private void VerwijderType()
        {
            if (SelectedType != null)
            {
                MessageBoxResult result = MessageBox.Show("Wil je " + SelectedType.Name.Trim() + " verwijderen?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    string message = ContactpersonType.DeleteFunctie(SelectedType);
                    if (message != null)
                    {
                        MessageBox.Show(message);
                    }
                    TypeList = ContactpersonType.GetTypes();
                }
            }
        }
        public ICommand ContactDeleteCommand
        {
            get{return new RelayCommand(DeleteContact);}
        }
        private void DeleteContact()
        {
            if (SelectedContact != null)
            {
                MessageBoxResult result = MessageBox.Show("Wil je " + SelectedContact.Name.Trim() + " verwijderen?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Contactperson.DeleteContact(SelectedContact);
                    ContactList = Contactperson.GetContactPersons();
                    CloseClickAll();
                }
            }
        }
      
        public ICommand ContactAddCommand
        {
            get{ return new RelayCommand(AddContact);}
        }
        private void AddContact()
        {
            Contactperson.AddContact(FormContact.Name, FormContact.Company, FormContact.JobRole, FormContact.City, FormContact.Email, FormContact.Phone, FormContact.CellPhone);
            CloseClickAll();
            ContactList = Contactperson.GetContactPersons();
        }
        public ICommand ContactEditCommand
        {
            get{ return new RelayCommand(EditContact);}
        }
        private void EditContact()
        {
            Contactperson.EditContact(FormContact.Name, FormContact.Company, FormContact.JobRole, FormContact.City, FormContact.Email, FormContact.Phone, FormContact.CellPhone, SelectedContact.ID);
            CloseClickAll();
            ContactList = Contactperson.GetContactPersons();
        }
        public ICommand ContactSearchCommand
        {
            get{ return new RelayCommand(ContactSearch); }
        }
        private void ContactSe                                                                                                                                                                                                  