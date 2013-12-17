using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.View;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
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
            GenreList = Genre.GetGenres();
            ListBands = Band.GetBands();
            AddContVis = "Hidden";

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
            AddContVis = "Visible";
            ToevoegShow = "Visible";
            EditShow = "Hidden";
            BandContVis = "Hidden";
        }
        
        #endregion
        #region AddForm
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
            OpenFileDialog ofd = new OpenFileDialog();
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
            if (ImagePath != "")
            {
                Band.CreateBand(FormBand, ImagePath);
            }
        }
        #endregion



    }
}
