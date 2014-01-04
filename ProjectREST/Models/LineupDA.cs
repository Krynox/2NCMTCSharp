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
    public class LineupDA
    {
        public static ObservableCollection<LineUp> GetLineUp()
        {
            ObservableCollection<LineUp> lineups = new ObservableCollection<LineUp>();

            DbDataReader reader = Database.GetData("SELECT tbl_lineup.ID as ID,DatePreformance,FromTime,UntilTime,Band,Stage FROM tbl_lineup ORDER BY FromTime"
                );
            foreach (IDataRecord db in reader)
            {
                lineups.Add(Create(db));
            }

            return lineups;
        }
        private static LineUp Create(IDataRecord db)
        {
            return new LineUp()
            {
                ID = db["ID"].ToString(),
                Date = Convert.ToDateTime(db["DatePreformance"]),
                From = db["FromTime"].ToString(),
                Until = db["UntilTime"].ToString(),
                Stage = StageDA.GetStage(db["Stage"].ToString()),
                Band = BandDA.GetBands(db["Band"].ToString()),
            };
        }
    }
}