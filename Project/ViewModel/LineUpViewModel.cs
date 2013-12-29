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
            GenreList = Genre.GetGenres();
            ListBands = Band.GetBands();
            AddContVis = "Hidden";
            GenreContVis = "Hidden";

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

        private void OpenWijzigGenreClick()
        {
            EditGenreVis = "Visible";
            AddGenreVis = "Hidden";
        }


        private void VerwijderGenreClick()
        {
            if (SelectedGenre != null)
            {
 
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
