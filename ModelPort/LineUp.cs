using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ModelPort
{
    public class LineUp
    {
        public string ID { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string Until { get; set; }
        public Stage Stage { get; set; }
        public Band Band { get; set; }
        

    }
}
