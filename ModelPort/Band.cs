using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace ModelPort
{
    public class Band 
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public byte[] Picture { get; set; }
        public string Description { get; set; }

        public string Twitter { get; set; }

        public string Facebook { get; set; }

        public ObservableCollection<Genre> Genres { get; set; }

        public Band()
        {
            Genres = new ObservableCollection<Genre>();
        }
    }
}
