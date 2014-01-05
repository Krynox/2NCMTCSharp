using ModelPort;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApp.DataModel
{
    public class BandVM : ObservableObject,INotifyPropertyChanged
    {
        private ObservableCollection<Band> _lstBand;

        public ObservableCollection<Band> LstBand
        {
            get { return _lstBand; }
            set { _lstBand = value; OnPropertyChanged("LstBand"); }
        }
        private Genre _selectedGenre;

        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set { _selectedGenre = value; OnPropertyChanged("SelectedGenre"); FilterBands(); }
        }
        
        public BandVM()
        {
            LstBand = new ObservableCollection<Band>();
            GetBands();
        }
        public async void GetBands()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = await client.GetAsync("http://localhost:10841/api/band");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(ObservableCollection<Band>));
                LstBand = dxml.ReadObject(stream) as ObservableCollection<Band>;
            }
        }
        private void FilterBands()
        {
            throw new NotImplementedException();
        }
        
    }
}
