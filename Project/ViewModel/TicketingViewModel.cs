using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
        private ObservableCollection<TicketType> ticketTypeList;

        public ObservableCollection<TicketType> TicketTypeList
        {
            get { return ticketTypeList; }
            set { ticketTypeList = value; }
        }
        private TicketType _selectedType;
        public TicketType SelectedType
        {
            get { return _selectedType; }
            set { _selectedType = value; OnPropertyChanged("SelectedType"); }
        }
    }
}
