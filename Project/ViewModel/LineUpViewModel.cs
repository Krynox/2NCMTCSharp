using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.View;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Project.ViewModel
{
    class LineUpViewModel : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Line-Up"; }
        }
        public LineUpViewModel()
        {
            FormBand = new Band();
            SelectedIndexStage = 0;
            GenreList = Genre.GetGenres();
            ListBands = Band.GetBands();
            AddContVis = "Hidden";
            GenreContVis = "Hidden";
            TimeList = GetTimes();
            DatumList = Festival.GetDates();
            StageList = Stage.GetStages();
            SelectedDatum = DatumList[0];
            SelectedStage = StageList[0];
            SelectedStartIndex = 0;
            SelectedEndIndex = 0;
            SelectedBandIndex = 0;
            AddLineUpVis = "Visible";
            EditLineUpVis = "Hidden";
            LineUpList = LineUp.GetLineUp(DatumList[0], StageList[0]);
        }
       
        
        
        private LineUp _selectedLineUp;

        public LineUp SelectedLineUp
        {
            get { return _selectedLineUp; }
            set { _selectedLineUp = value; OnPropertyChanged("SelectedLineUp"); }
        }

        private ObservableCollection<LineUp> _lineUpList;

        public ObservableCollection<LineUp> LineUpList
        {
            get { return _lineUpList; }
            set { _lineUpList = value; OnPropertyChanged("LineUpList"); }
        }
        
        private int _selectedIndexStage;

        public int SelectedIndexStage
        {
            get { return _selectedIndexStage; }
            set { _selectedIndexStage = value; OnPropertyChanged("SelectedIndexStage"); }
        }
        
        private ObservableCollection<Stage> _stageList;

        public ObservableCollection<Stage> StageList
        {
            get { return _stageList; }
            set { _stageList = value; OnPropertyChanged("StageList"); }
        }
        private Stage _selectedStage;

        public Stage SelectedStage
        {
            get { return _selectedStage; }
            set { _selectedStage = value; OnPropertyChanged("SelectedStage"); if (value != null) { LineUpList = LineUp.GetLineUp(SelectedDatum, SelectedStage); } }
        }
        
        
        private ObservableCollection<DateTime> _datumList;

        public ObservableCollection<DateTime> DatumList
        {
            get { return _datumList; }
            set { _datumList = value; OnPropertyChanged("DatumList"); }
        }
        private DateTime _selectedDatum;

        public DateTime SelectedDatum
        {
            get { return _selectedDatum; }
            set { _selectedDatum = value; OnPropertyChanged("SelectedDatum"); if (value != null) { LineUpList = LineUp.GetLineUp(SelectedDatum, SelectedStage); } }
        }
        
        private ObservableCollection<string> GetTimes()
        {
            ObservableCollection<string> times = new ObservableCollection<string>();
            int uren = 0;
            int minuten = 0;
            for (int i = 0; i < 47; i++)
            {
                if (i % 2 == 0)
                {
                    if (i >= 2)
                    {
                        uren++;
                    }
                    minuten = 0;
                    if (i <18)
                    {
                        if (minuten == 30)
                        {
                            times.Add("0" + uren + ":" + minuten);
                        }
                        else {
                            times.Add("0" + uren + ":" + minuten+"0");
                        }
                    }
                    else
                    {
                        if (minuten == 30)
                        {
                            times.Add(uren + ":" + minuten);
                        }
                        else
                        {
                            times.Add(uren + ":" + minuten + "0");
                        }
                    }
                }
                else
                {

                    minuten = 30;
                    if (i < 18)
                    {
                        times.Add("0"+uren + ":" + minuten);
                    }
                    else {
                        times.Add(uren + ":" + minuten);
                    }
                    
                }
            }
            return times;
        }
        private ObservableCollection<string> _timeList;
        public ObservableCollection<string> TimeList
        {
            get { return _timeList; }
            set { _timeList = value; OnPropertyChanged("TimeList"); }
        }
        private string _addLineUpVis;

        public string AddLineUpVis
        {
            get { return _addLineUpVis; }
            set { _addLineUpVis = value; OnPropertyChanged("AddLineUpVis"); }
        }
        private string _editLineUpVis;

        public string EditLineUpVis
        {
            get { return _editLineUpVis; }
            set { _editLineUpVis = value; OnPropertyChanged("EditLineUpVis"); }
        }
        
        private int _selectedStartIndex;

        public int SelectedStartIndex
        {
            get { return _selectedStartIndex; }
            set { _selectedStartIndex = value; OnPropertyChanged("SelectedStartIndex"); }
        }
        private int _selectedEndIndex;

        public int SelectedEndIndex
        {
            get { return _selectedEndIndex; }
            set { _selectedEndIndex = value; OnPropertyChanged("SelectedEndIndex"); }
        }
        private int _selectedBandIndex;

        public int SelectedBandIndex
        {
            get { return _selectedBandIndex; }
            set { _selectedBandIndex = value; OnPropertyChanged("SelectedBandIndex"); }
        }
       
        private Band _selectedBandAdd;

        public Band SelectedBandAdd
        {
            get { return  _selectedBandAdd; }
            set { _selectedBandAdd = value; OnPropertyChanged("SelectedBandAdd"); }
        }
        private string _endTime;

        public string EndTime
        {
            get { return _endTime; }
            set { _endTime = value; OnPropertyChanged("EndTime"); }
        }
        private string _startTime;

        public string StartTime
        {
            get { return _startTime; }
            set { _startTime = value; OnPropertyChanged("StartTime"); }
        }
        
        private Band _formBand;
        public Band FormBand
        {
            get { return _formBand; }
            set { _formBand = value; OnPropertyChanged("FormBand"); }
        }
        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged("ImagePath"); }
        }
        private string _zoekBands;

        public string ZoekBands
        {
            get { return _zoekBands; }
            set { _zoekBands = value; OnPropertyChanged("ZoekBands"); ZoekBandsOp(); }
        }

        #region Lists
        private ObservableCollection<Band> _listBands;

        public ObservableCollection<Band> ListBands
        {
            get { return _listBands; }
            set { _listBands = value; OnPropertyChanged("ListBands"); }
        }
        
        private ObservableCollection<Genre> _genreList;

        public ObservableCollection<Genre> GenreList
        {
            get { return _genreList; }
            set { _genreList = value; OnPropertyChanged("GenreList"); }
        }
        
        #endregion
        #region Selected
        private Band _selectedBand;

        public Band SelectedBand
        {
            get { return _selectedBand; }
            set { _selectedBand = value; OnPropertyChanged("SelectedBand"); }
        }
        
        private Genre _selectedGenre;

        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set { _selectedGenre = value; OnPropertyChanged("SelectedGenre"); }
        }
        private Genre _addedSlectedGenre;

        public Genre AddedSlectedGenre
        {
            get { return _addedSlectedGenre; }
            set { _addedSlectedGenre = value; OnPropertyChanged("AddedSlectedGenre"); }
        }
        
        #endregion
        #region Controls
        private string _editGenre;

        public string EditGenre
        {
            get { return _editGenre; }
            set { _editGenre = value; OnPropertyChanged("EditGenre"); }
        }
        
        private string _editGenreVis;

        public string EditGenreVis
        {
            get { return _editGenreVis; }
            set { _editGenreVis = value; OnPropertyChanged("EditGenreVis"); }
        }
        private string _addGenreVis;

        public string AddGenreVis
        {
            get { return _addGenreVis; }
            set { _addGenreVis = value; OnPropertyChanged("AddGenreVis"); }
        }
        
        
        private string _genreContVis;

        public string GenreContVis
        {
            get { return _genreContVis; }
            set { _genreContVis = value; OnPropertyChanged("GenreContVis"); }
        }
        
        private string _bandContVis;

        public string BandContVis
        {
            get { return _bandContVis; }
            set { _bandContVis = value; OnPropertyChanged("BandContVis"); }
        }
        
        private string _toevoegShow;

        public string ToevoegShow
        {
            get { return _toevoegShow; }
            set { _toevoegShow = value; OnPropertyChanged("ToevoegShow"); }
        }
        private string _editShow;

        public string EditShow
        {
            get { return _editShow; }
            set { _editShow = value; OnPropertyChanged("EditShow"); }
        }
        
        
        private string _addContVis;

        public string AddContVis
        {
            get { return _addContVis; }
            set { _addContVis = value; OnPropertyChanged("AddContVis"); }
        }
        public ICommand ToevoegContClick
        {
            get { return new RelayCommand(ToevoegContShow); }
        }
        public ICommand CloseAdd
        {
            get { return new RelayCommand(CloseAddFunc); }
        }

        private void CloseAddFunc()
        {
            AddContVis = "Hidden";
            BandContVis="Visible";
        }

        private void ToevoegContShow()
        {
            FormBand = new Band();
            AddContVis = "Visible";
            ToevoegShow = "Visible";
            EditShow = "Hidden";
            BandContVis = "Hidden";
        }
        
        #endregion
        #region AddForm
        public ICommand ZoekReserveringClick
        {
            get { return new RelayCommand(SaveBand); }
        }
        public ICommand BrowseClick
        {
            get { return new RelayCommand(BrowseFile); }
        }
        public ICommand GerneAddClick
        {
            get { return new RelayCommand(AddGenre); }
        }
        public ICommand GenreRemoveClick
        {
            get { return new RelayCommand(RemoveGenre); }
        }
        public ICommand OpslaanBand
        {
            get { return new RelayCommand(SaveBand); }
        }
        public ICommand VerwijderbandClick
        {
            get { return new RelayCommand(Verwijderband); }
        }
        public ICommand OpenWijzigClick
        {
            get { return new RelayCommand(OpenWijzig); }
        }
        public ICommand WijzigBand
        {
            get { return new RelayCommand(EditBand); }
        }
        public ICommand OpenGenres
        {
            get { return new RelayCommand(OpenGernesClick); }
        }
        public ICommand CloseGenre
        {
            get { return new RelayCommand(CloseGernesClick); }
        }
        public ICommand VerwijderGenre
        {
            get { return new RelayCommand(VerwijderGenreClick); }
        }
        public ICommand OpenWijzigGenre
        {
            get { return new RelayCommand(OpenWijzigGenreClick); }
        }
        public ICommand OpenToevoegenGenre
        {
            get { return new RelayCommand(OpenToevoegenGenreClick); }
        }
        public ICommand WijzigGenreOpslaanClick
        {
            get { return new RelayCommand(WijzigGenreOpslaan); }
        }
        public ICommand ToevoegenGenreOpslaanClick
        {
            get { return new RelayCommand(ToevoegenGenreOpslaan); }
        }
        public ICommand OpslaanLineup
        {
            get { return new RelayCommand(OpslaanLineupClick); }
        }
        public ICommand VerwijderLineupClick
        {
            get { return new RelayCommand(VerwijderLineup); }
        }
        public ICommand ShowLineUpAddClick
        {
            get { return new RelayCommand(ShowLineUpAdd); }
        }
        public ICommand EditLineup
        {
            get { return new RelayCommand(EditLineUpClick); }
        }

        private void EditLineUpClick()
        {
            if (SelectedLineUp != null)
            {
                string message = LineUp.EditLineUp(SelectedBandAdd, StartTime, EndTime, SelectedStage, SelectedDatum, SelectedLineUp.ID);
                if (message != null)
                {
                    System.Windows.Forms.MessageBox.Show(message);
                }
                LineUpList = LineUp.GetLineUp(SelectedDatum, SelectedStage);
            }

        }
        private void ShowLineUpAdd()
        {
            SelectedStartIndex = 0;
            SelectedEndIndex = 0;
            SelectedBandIndex = 0;
            AddLineUpVis = "Visible";
            EditLineUpVis = "Hidden";
        }
        public ICommand ShowLineUpEditClick
        {
            get { return new RelayCommand(ShowLineUpEdit); }
        }

        private void ShowLineUpEdit()
        {
            int index=0;
            foreach(Band b in ListBands)
            {
                if (SelectedLineUp.Band.Name == b.Name)
                {
                    SelectedBandIndex = index;
                }
                index++;
            }
            index = 0;
            foreach(string t in TimeList)
            {
                if(t==SelectedLineUp.From.Trim())
                {
                    SelectedStartIndex = index;
                }
                if (t == SelectedLineUp.Until.Trim())
                {
                    SelectedEndIndex = index;
                }
                index++;
            }
            AddLineUpVis = "Hidden";
            EditLineUpVis = "Visible";
        }

        private void VerwijderLineup()
        {
            if (SelectedLineUp != null)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Wil je " + SelectedLineUp.From.Trim() + " tot " + SelectedLineUp.From.Trim() + " verwijderen?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    LineUp.DeleteLineUp(SelectedLineUp);
                    LineUpList = LineUp.GetLineUp(SelectedDatum, SelectedStage);
                }
            }
        }
        private void OpslaanLineupClick()
        {
            string message =LineUp.AddLineUp(SelectedBandAdd,StartTime,EndTime,SelectedStage,SelectedDatum);
            if (message != null)
            {
                System.Windows.Forms.MessageBox.Show(message);
            }
            LineUpList = LineUp.GetLineUp(SelectedDatum, SelectedStage);
        }
        private void WijzigGenreOpslaan()
        {
            Genre.EditGenre(EditGenre,SelectedGenre.GenreName);
            GenreList = Genre.GetGenres();
            EditGenreVis = "Hidden";
            AddGenreVis = "Hidden";
        }
        private void ToevoegenGenreOpslaan()
        {
            Genre.SaveGenre(EditGenre);
            GenreList = Genre.GetGenres();
            EditGenreVis = "Hidden";
            AddGenreVis = "Hidden";
        }

        private void OpenToevoegenGenreClick()
        {
            EditGenreVis = "Hidden";
            AddGenreVis = "Visible";
            EditGenre = "";
        }

        private void OpenWijzigGenreClick()
        {
            if (SelectedGenre != null)
            { 
                EditGenreVis = "Visible";
                AddGenreVis = "Hidden";
                EditGenre=SelectedGenre.GenreName.Trim();
            }

        }


        private void VerwijderGenreClick()
        {
            if (SelectedGenre != null)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Wil je " + SelectedGenre.GenreName.Trim() + " verwijderen?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    string message = Genre.DeleteGenre(SelectedGenre);
                    if (message != null)
                    {
                        System.Windows.MessageBox.Show(message);
                    }
                    GenreList = Genre.GetGenres();
                }
            }
        }

        private void CloseGernesClick()
        {
            AddContVis = "Hidden";
            BandContVis = "Visible";
            GenreContVis = "Hidden";
        }
        private void OpenGernesClick()
        {
            AddContVis = "Hidden";
            BandContVis = "Hidden";
            GenreContVis = "Visible";
            EditGenreVis = "Hidden";
            AddGenreVis = "Hidden";
        }

        private void EditBand()
        {
            Band.EditBand(SelectedBand,ImagePath);
        }

        private void OpenWijzig()
        {
            if (SelectedBand != null)
            {
                AddContVis = "Visible";
                BandContVis = "Hidden";
                EditShow = "Visible";
                ToevoegShow = "Hidden";
                FormBand = SelectedBand;
            }
        }


        private void Verwijderband()
        {
            if (SelectedBand != null)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Wil je " + SelectedBand.Name.Trim() + " verwijderen?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    string message = Band.DeleteBand(SelectedBand);
                    if (message != null)
                    {
                        System.Windows.MessageBox.Show(message);
                    }
                    ListBands = Band.GetBands();
                }
            }
        }
        private void AddGenre()
        {
            bool dupe = false;
            foreach (Genre s in FormBand.Genres)
            {
                if (s.GenreName == SelectedGenre.GenreName)
                {
                    dupe = true;
                }
            }
            if (dupe == false)
            {
                FormBand.Genres.Add(SelectedGenre);
            }
        }
        private void RemoveGenre()
        {
            FormBand.Genres.Remove(AddedSlectedGenre);
        }
        private void BrowseFile()
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter="JPEG (.jpg)|*.jpg|PNG (.png)|*.png";
            ofd.Multiselect = false;
            ofd.FileName = String.Empty;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ImagePath = ofd.FileName;
            }
        }
        private void SaveBand()
        {
            if (ImagePath != null)
            {
                Band.CreateBand(FormBand, ImagePath);
                AddContVis = "Hidden";
                BandContVis = "Visible";
            }
        }
        private void ZoekBandsOp()
        {
            ListBands = Band.GetBandsSearch(ZoekBands);
        }
        #endregion



    }
}
