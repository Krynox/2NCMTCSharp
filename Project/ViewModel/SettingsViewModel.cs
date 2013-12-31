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
    class SettingsViewModel : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Settings"; }
        }
        public SettingsViewModel()
        {
            StageList = Stage.GetStages();
            AddVis = "Visible";
            EditVis="Hidden";
            TxtStartDate = Festival.GetEndStart().StartDate.ToShortDateString();
            TxtEndDate = Festival.GetEndStart().EndDate.ToShortDateString();
            DtStartDate = DateTime.Now;
            DtEndDate = DateTime.Now;
        }
        private DateTime _dtStartDate;

        public DateTime DtStartDate
        {
            get { return _dtStartDate; }
            set { _dtStartDate = value; OnPropertyChanged("DtStartDate"); }
        }
        private DateTime _dtEndDate;

        public DateTime DtEndDate
        {
            get { return _dtEndDate; }
            set { _dtEndDate = value; OnPropertyChanged("DtEndDate"); }
        }
        
        private string _txtStartDate;

        public string TxtStartDate
        {
            get { return _txtStartDate; }
            set { _txtStartDate = value; OnPropertyChanged("TxtStartDate"); }
        }
        private string _txtEndDate;

        public string TxtEndDate
        {
            get { return _txtEndDate; }
            set { _txtEndDate = value; OnPropertyChanged("TxtEndDate"); }
        }
        private Stage _selectedStage;

        public Stage SelectedStage
        {
            get { return _selectedStage; }
            set { _selectedStage = value; OnPropertyChanged("SelectedStage"); }
        }
        
        private ObservableCollection<Stage> _stageList;

        public ObservableCollection<Stage> StageList
        {
            get { return _stageList; }
            set { _stageList = value; OnPropertyChanged("StageList"); }
        }
        private string _editVis;

        public string EditVis
        {
            get { return _editVis; }
            set { _editVis = value; OnPropertyChanged("EditVis"); }
        }
        private string _addVis;

        public string AddVis
        {
            get { return _addVis; }
            set { _addVis = value; OnPropertyChanged("AddVis"); }
        }
        private string _txtStage;

        public string TxtStage
        {
            get { return _txtStage; }
            set { _txtStage = value; OnPropertyChanged("TxtStage"); }
        }
        public ICommand EditDatesClick
        {
            get { return new RelayCommand(EditDates); }
        }

        private void EditDates()
        {
            if (DtEndDate > DtStartDate)
            {
                if (DtEndDate >= DateTime.Now && DtStartDate >= DateTime.Now)
                {
                    Festival.EditDates(DtStartDate, DtEndDate);
                    TxtStartDate = Festival.GetEndStart().StartDate.ToShortDateString();
                    TxtEndDate = Festival.GetEndStart().EndDate.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("Deze datums zijn al reeds voorbij");
                }
            }
            else
            {
                MessageBox.Show("De einddatum is kleiner dan de startdatum");
            }

        }

        public ICommand ToevoegenOpenClick
        {
            get { return new RelayCommand(ToevoegenOpen); }
        }

        private void ToevoegenOpen()
        {
            TxtStage = "";
            AddVis = "Visible";
            EditVis = "Hidden";
        }
        public ICommand WijzigenOpenClick
        {
            get { return new RelayCommand(WijzigenOpen); }
        }

        private void WijzigenOpen()
        {
            if (SelectedStage != null)
            {
                TxtStage = SelectedStage.Name;
                AddVis = "Hidden";
                EditVis = "Visible";
            }

        }
        public ICommand StageToevoegenClick
        {
            get { return new RelayCommand(StageToevoegen); }
        }

        private void StageToevoegen()
        {
            Stage.AddStage(TxtStage);
            StageList = Stage.GetStages();
        }
        public ICommand StageWijzigenClick
        {
            get { return new RelayCommand(StageWijzigen); }
        }

        private void StageWijzigen()
        {
            Stage.EditStage(TxtStage,SelectedStage);
            TxtStage = "";
            StageList = Stage.GetStages();
        }
        
        public ICommand VerwijderStageClick
        {
            get { return new RelayCommand(VerwijderStage); }
        }

        private void VerwijderStage()
        {
            if (SelectedStage != null)
            {
                MessageBoxResult result = MessageBox.Show("Wil je " + SelectedStage.Name.Trim() + " verwijderen?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    string message = Stage.DeleteStage(SelectedStage);
                    if (message != null)
                    {
                        MessageBox.Show(message);
                    }
                    StageList = Stage.GetStages();
                }
            }
        }
        
    }
}
