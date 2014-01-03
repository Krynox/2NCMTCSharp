using Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace ProjectSite.ViewModel
{
    public class LineUpVM
    {
        public ObservableCollection<DateTime> Dates { get; set; }
        public ObservableCollection<LineUp> LineUpList { get; set; }
        public ObservableCollection<Stage> StageList { get; set; }
        public LineUpVM()
        {
            Dates = Festival.GetDates();
            LineUpList = LineUp.GetLineUp(Dates[0]);
            StageList = Stage.GetStages();
        }
        public LineUpVM(DateTime date)
        {
            Dates = Festival.GetDates();
            LineUpList = LineUp.GetLineUp(date);
            StageList = Stage.GetStages();
        }
    }
}