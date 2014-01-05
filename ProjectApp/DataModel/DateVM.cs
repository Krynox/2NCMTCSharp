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
    public class DateVM: ObservableObject, INotifyPropertyChanged
    {
        private ObservableCollection<DateTime> _lstDates;

        public ObservableCollection<DateTime> LstDates
        {
            get { return _lstDates; }
            set { _lstDates = value; OnPropertyChanged("LstDates"); }
        }
        public DateVM ()
        {
            LstDates = new ObservableCollection<DateTime>();
            GetDates();
        }
        public async void GetDates()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = await client.GetAsync("http://localhost:10841/api/date");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(ObservableCollection<DateTime>));
                LstDates = dxml.ReadObject(stream) as ObservableCollection<DateTime>;
            }
        }
    }
}
