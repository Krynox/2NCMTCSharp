using Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ProjectREST.Models
{
    public class FestivalDA
    {
        public static ObservableCollection<DateTime> GetDates()
        {
            ObservableCollection<DateTime> dates = new ObservableCollection<DateTime>();
            DateTime endDate = new DateTime();
            DateTime startDate = new DateTime();
            TimeSpan timebetweendates = new TimeSpan();
            int between = 0;
            endDate = GetEndStart().EndDate;
            startDate = GetEndStart().StartDate;
            timebetweendates = endDate - startDate;
            between = Convert.ToInt32(timebetweendates.TotalDays);
            for (int i = 0; i <= between; i++)
            {
                dates.Add(startDate.AddDays(i));
            }
            return dates;
        }
        public static Festival GetEndStart()
        {
            Festival fest = new Festival();
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_startEnd");
            foreach (IDataRecord db in reader)
            {
                fest.StartDate = Convert.ToDateTime(db["StartDate"]);
                fest.EndDate = Convert.ToDateTime(db["EndDate"]);
            }
            reader.Close();
            return fest;
        }
    }
}