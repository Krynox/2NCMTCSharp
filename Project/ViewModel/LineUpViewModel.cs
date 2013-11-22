using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.View;

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

            
        }
        

    }
}
