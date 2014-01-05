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
    public class GenreVM : ObservableObject, INotifyPropertyChanged
    {
        private ObservableCollection<Genre> _lstGenre;

        public ObservableCollection<Genre> LstGenre
        {
            get { return _lstGenre; }
            set { _lstGenre = value; OnPropertyChanged("LstGenre"); }
        }
        public GenreVM()
        {
            LstGenre = new ObservableCollection<Genre>();
            GetGenre();
        }
        public async void GetGenre()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = await client.GetAsync("http://localhost:10841/api/genre");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(ObservableCollection<Genre>));
                LstGenre = dxml.ReadObject(stream) as ObservableCollection<Genre>;
            }
        }
        
    }
}
