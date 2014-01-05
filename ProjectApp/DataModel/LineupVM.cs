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
    public class LineupVM : ObservableObject, INotifyPropertyChanged
    {
        private ObservableCollection<LineUp> _lstLineup;

        public ObservableCollection<LineUp> LstLineup
        {
            get { return _lstLineup; }
            set { _lstLineup = value; OnPropertyChanged("LstLineup"); }
        }
        public LineupVM()
        {
            LstLineup = new ObservableCollection<LineUp>();
            GetLineup();
        }
        public async void GetLineup()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = await client.GetAsync("http://localhost:10841/api/lineup");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(ObservableCollection<LineUp>));
                LstLineup = dxml.ReadObject(stream) as ObservableCollection<LineUp>;
            }
        }
        
    }
}
