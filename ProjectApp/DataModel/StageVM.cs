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
    public class StageVM: ObservableObject, INotifyPropertyChanged
    {
        private ObservableCollection<Stage> _lstStages;

        public ObservableCollection<Stage> LstStages
        {
            get { return _lstStages; }
            set { _lstStages = value; OnPropertyChanged("LstStages"); }
        }
        public StageVM ()
	    {
            LstStages= new ObservableCollection<Stage>();
            GetStages();
	    }
        public async void GetStages()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = await client.GetAsync("http://localhost:10841/api/stage");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(ObservableCollection<Stage>));
                LstStages = dxml.ReadObject(stream) as ObservableCollection<Stage>;
            }
        }
    }
}
