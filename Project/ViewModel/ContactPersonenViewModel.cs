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
        }

        #region Lists

        private ObservableCollection<ContactpersonType> _typeList;
        public ObservableCollection<ContactpersonType> TypeList
        {
            get { return _typeList; }
            set { _typeList = value; }
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
            set { _selectedContact = value; }
        }
        
        #endregion

        #region FormWaarden
        private string _contactName;

        public string ContactName
        {
            get { return _contactName; }
            set { _contactName = value; OnPropertyChanged("ContactName"); }
        }
        private string _contactCompany;

        public string ContactCompany
        {
            get { return _contactCompany; }
            set { _contactCompany = value; OnPropertyChanged("ContactCompany"); }
        }
        private string _contactCity;

        public string ContactCity
        {
            get { return _contactCity; }
            set { _contactCity = value; OnPropertyChanged("ContactCity"); }
        }
        private string _contactEmail;

        public string ContactEmail
        {
            get { return _contactEmail; }
            set { _contactEmail = value; OnPropertyChanged("ContactEmail"); }
        }
        private string _contactPhone;

        public string ContactPhone
        {
            get { return _contactPhone; }
            set { _contactPhone = value; OnPropertyChanged("ContactPhone"); }
        }
        private string _contactCellPhone;

        public string ContactCellPhone
        {
            get { return _contactCellPhone; }
            set { _contactCellPhone = value; OnPropertyChanged("ContactCellPhone"); }
        }
        private ContactpersonType _contactJobRole;

        public ContactpersonType ContactJobRole
        {
            get { return _contactJobRole; }
            set { _contactJobRole = value; OnPropertyChanged("ContactJobRole");}
        }
        #endregion

        #region Controls
        private string _addControls;

        public string AddControls
        {
            get { return _addControls; }
            set { _addControls = value;OnPropertyChanged("AddControls"); }
        }
        private string _editControls;

        public string EditControls
        {
            get { return _editControls; }
            set { _editControls = value; OnPropertyChanged("EditControls"); }
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
        private string _closeVis;
        public string CloseVis
        {
            get { return _closeVis; }
            set { _closeVis = value; OnPropertyChanged("CloseVis");}
        }
        private void CloseClickAll()
        {
            MenuHeightAdd = 0;
            MenuZoekHeight = 0;
            CloseVis = "Hidden";
            ContactName = "";
            ContactCompany = "";
            ContactCity = "";
            ContactEmail = "";
            ContactJobRole = null;
            ContactPhone = "";
            ContactCellPhone = "";
        }
        public ICommand CloseClick
        {
            get
            { return new RelayCommand(CloseClickAll); }
        }
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

        }
        public ICommand EditMenuClick
        {
            get
            { return new RelayCommand(EditMenu); }
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
                ContactName = SelectedContact.Name.Trim();
                ContactCompany = SelectedContact.Company.Trim();
                ContactCity = SelectedContact.City.Trim();
                ContactEmail = SelectedContact.Email.Trim();
                ContactJobRole = SelectedContact.JobRole;
                ContactPhone = SelectedContact.Phone.Trim();
                ContactCellPhone = SelectedContact.CellPhone.Trim();

            }
        }
        public ICommand ZoekClick
        {
            get
            { return new RelayCommand(Zoek); }
        }
        private void Zoek()
        {
            CloseClickAll();
            MenuZoekHeight = 250;
            CloseVis = "Visible";
        }

        #endregion

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
            Contactperson.AddContact(ContactName, ContactCompany, ContactJobRole, ContactCity, ContactEmail, ContactPhone, ContactCellPhone);
            CloseClickAll();
            ContactList = Contactperson.GetContactPersons();
        }
        public ICommand ContactEditCommand
        {
            get{ return new RelayCommand(EditContact);}
        }
        private void EditContact()
        {
            Contactperson.EditContact(ContactName, ContactCompany, ContactJobRole, ContactCity, ContactEmail, ContactPhone, ContactCellPhone,SelectedContact.ID);
            CloseClickAll();
            ContactList = Contactperson.GetContactPersons();
        }
        public ICommand ContactSearchCommand
        {
            get{ return new RelayCommand(ContactSearch); }
        }
        private void ContactSearch()
        {
            ContactList=Contactperson.ZoekContact(ContactName, ContactCompany, ContactJobRole);
            CloseClickAll();
        }
        
    }
}
