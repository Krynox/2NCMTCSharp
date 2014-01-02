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
            ContactList = Contactperson.GetContactPersons();
            TypeList = ContactpersonType.GetTypes();
            AddControls = "Visible";
            EditControls = "Hidden";
            CloseVis = "Hidden";
            WindowHeight = 600;
            FormContact = new Contactperson();
            ContactTypeAdd = new ContactpersonType();
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

        private ContactpersonType _contactTypeAdd;

        public ContactpersonType ContactTypeAdd
        {
            get { return _contactTypeAdd; }
            set { _contactTypeAdd = value; OnPropertyChanged("ContactTypeAdd"); }
        }
        #endregion

        #region VisibilityProps
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

        #region HeightProps
        private int _windowHeight;

        public int WindowHeight
        {
            get { return _windowHeight; }
            set { _windowHeight = value; OnPropertyChanged("WindowHeight"); }
        }
        
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

        #region CloseAll
        public ICommand CloseClick
        {
            get
            { return new RelayCommand(CloseClickAll); }
        }
        private void CloseClickAll()
        {
            MenuHeightAdd = 0;
            MenuZoekHeight = 0;
            FunctieMenuHeight = 0;
            CloseVis = "Hidden";
            FormContact = new Contactperson();
            ListIndex = 0;
            WindowHeight = 700;
        }
        #endregion

        #region FunctieOpenen
        public ICommand FunctieClick
        {
            get
            { return new RelayCommand(OpenFunctie); }
        }
        private void OpenFunctie()
        {
            CloseClickAll();
            FunctieMenuHeight = 300;
            CloseVis = "Visible";
            AddTypeVis = "Hidden";
            WindowHeight = 300;
        }
        #endregion

        #region FunctieOpenToevoegenApart
        public ICommand AddTypeClick
        {
            get
            { return new RelayCommand(AddTypeControl); }
        }
        private void AddTypeControl()
        {
            AddTypeVis = "Visible";
            EditTypeKnop = "Hidden";
            AddTypeKnop = "Visible";
            ContactTypeAdd = new ContactpersonType();
        }
        #endregion

        #region FunctieOpenEditApart
        public ICommand EditTypeClick
        {
            get
            { return new RelayCommand(EditType, IsTypeSelected); }
        }
        private void EditType()
        {
            if (SelectedType != null)
            {
                AddTypeVis = "Visible";
                EditTypeKnop = "Visible";
                AddTypeKnop = "Hidden";
                ContactTypeAdd = SelectedType;
            }
        }
        #endregion

        #region FunctieOpslaanApart
        public ICommand AddTypeClickOpslaan
        {
            get
            { return new RelayCommand(AddTypeOpslaan, IsTypeComplete); }
        }
        private void AddTypeOpslaan()
        {
            ContactpersonType.AddContact(ContactTypeAdd.Name);
            AddTypeVis = "Hidden";
            TypeList = ContactpersonType.GetTypes();
            ContactTypeAdd = new ContactpersonType();
        }
        #endregion

        #region FunctieEditApart
        public ICommand EditTypeClickOpslaan
        {
            get
            { return new RelayCommand(EditTypeOpslaan, IsTypeComplete); }
        }
        private void EditTypeOpslaan()
        {
            ContactpersonType.EditType(SelectedType, ContactTypeAdd.Name);
            AddTypeVis = "Hidden";
            TypeList = ContactpersonType.GetTypes();
            ContactTypeAdd = new ContactpersonType();
        }
        #endregion

        #region FunctieVerwijderen
        public ICommand VerwijderTypeClick
        {
            get
            { return new RelayCommand(VerwijderType, IsTypeSelected); }
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
        #endregion

        #region AddTypeOpenInbedded
        public ICommand TypeAddCommand
        {
            get
            { return new RelayCommand(TypeAddShow); }
        }
        private void TypeAddShow()
        {
            AddType = "Visible";
            ContactTypeAdd = new ContactpersonType();
        }
        #endregion

        #region AddTypeInbedded
        public ICommand AddContactType
        {
            get
            { return new RelayCommand(ContactTypeToevoegen, IsTypeComplete); }
        }
        private void ContactTypeToevoegen()
        {
            ContactpersonType.AddContact(ContactTypeAdd.Name);
            AddType = "Hidden";
            TypeList = ContactpersonType.GetTypes();
            ContactTypeAdd = new ContactpersonType();
        }
        #endregion

        #region OpenAddMenu
        public ICommand AddMenuClick
        {
            get
            { return new RelayCommand(AddMenu); }
        }
        private void AddMenu()
        {
            CloseClickAll();
            MenuHeightAdd = 400;
            AddControls = "Visible";
            EditControls = "Hidden";
            CloseVis = "Visible";
            AddType = "Hidden";
            WindowHeight = 200;
        }
        #endregion

        #region OpenEditMenu
        public ICommand EditMenuClick
        {
            get { return new RelayCommand(EditMenu, IsContactSelected); }
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
                WindowHeight = 200;
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
        #endregion

        #region OpenZoekMenu
        public ICommand ZoekClick
        {
            get { return new RelayCommand(Zoek); }
        }
        private void Zoek()
        {
            CloseClickAll();
            MenuZoekHeight = 250;
            CloseVis = "Visible";
            WindowHeight = 400;
        }
        #endregion

        #region ContactPersoonVerwijderen
        public ICommand ContactDeleteCommand
        {
            get { return new RelayCommand(DeleteContact, IsContactSelected); }
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
        #endregion

        #region ContactPersoonToevoegen
        public ICommand ContactAddCommand
        {
            get { return new RelayCommand(AddContact, IsContactComplete); }
        }

        private void AddContact()
        {
            Contactperson.AddContact(FormContact.Name, FormContact.Company, FormContact.JobRole, FormContact.City, FormContact.Email, FormContact.Phone, FormContact.CellPhone);
            CloseClickAll();
            ContactList = Contactperson.GetContactPersons();
        }
        #endregion

        #region ContactPersoonWijzigen
        public ICommand ContactEditCommand
        {
            get { return new RelayCommand(EditContact, IsContactComplete); }
        }

        private void EditContact()
        {
            Contactperson.EditContact(FormContact.Name, FormContact.Company, FormContact.JobRole, FormContact.City, FormContact.Email, FormContact.Phone, FormContact.CellPhone, SelectedContact.ID);
            CloseClickAll();
            ContactList = Contactperson.GetContactPersons();
        }
        #endregion

        #region ContactZoek
        public ICommand ContactSearchCommand
        {
            get { return new RelayCommand(ContactSearch); }
        }

        private void ContactSearch()
        {
            ContactList = Contactperson.ZoekContact(FormContact.Name, FormContact.Company, FormContact.JobRole);
            CloseClickAll();
        }
        #endregion

        private bool IsTypeSelected()
        {
            if (SelectedType!=null)
            {
                return true;
            }
            else {
                return false;
            }
        }
        private bool IsContactSelected()
        {
            if (SelectedContact != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //IsValid Werkt niet in deze configuartie dus heb ik een kleine oplossing geschreven (niet volledig er zit geen datavalidatie in)
        private bool IsContactComplete()
        {
            if (FormContact.Name != null && FormContact.Phone != null && FormContact.JobRole != null && FormContact.Email != null && FormContact.Company != null && FormContact.City != null && FormContact.CellPhone != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsTypeComplete()
        { 
            if(ContactTypeAdd.Name!=null)
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
