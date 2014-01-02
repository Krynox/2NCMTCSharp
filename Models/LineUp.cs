using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class LineUp : IDataErrorInfo
    {
        public string ID { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string Until { get; set; }
        public Stage Stage { get; set; }
        [Required]
        public Band Band { get; set; }
        public static ObservableCollection<LineUp> GetLineUp(DateTime SelectedDatum, Stage SelectedStage)
        {
            ObservableCollection<LineUp> lineups = new ObservableCollection<LineUp>();
            if (SelectedStage != null && SelectedDatum != null)
            {
                DbDataReader reader = Database.GetData("SELECT tbl_lineup.ID as ID,DatePreformance,FromTime,UntilTime,Band FROM tbl_lineup WHERE Stage=@stageid AND DatePreformance=@date ORDER BY FromTime",
                    Database.AddParameter("@stageid", Convert.ToInt32(SelectedStage.ID)),
                    Database.AddParameter("@date", Convert.ToDateTime(SelectedDatum))
                    );
                foreach (IDataRecord db in reader)
                {
                    lineups.Add(Create(db, SelectedStage));
                }
            }
            return lineups;
        }

        private static LineUp Create(IDataRecord db, Stage SelectedStage)
        {
            return new LineUp()
            {
                ID = db["ID"].ToString(),
                Date = Convert.ToDateTime(db["DatePreformance"]),
                From = db["FromTime"].ToString(),
                Until = db["UntilTime"].ToString(),
                Stage = SelectedStage,
                Band = Band.GetBands(db["Band"].ToString()),
            };
        }

        public static string AddLineUp(Band SelectedBandAdd, string StartTime, string EndTime, Stage SelectedStage, DateTime SelectedDatum)
        {
            string error = "";
            error = CheckTime(StartTime, EndTime, SelectedStage,SelectedDatum);
            if (error == null)
            {
                error = CheckBand(StartTime, EndTime, SelectedStage, SelectedBandAdd,SelectedDatum);
                if (error == null)
                {
                    Database.ModifyData("INSERT INTO tbl_lineup (DatePreformance,FromTime,UntilTime,Stage,Band) VALUES (@date,@from,@until,@stage,@band)",
                        Database.AddParameter("@date", SelectedDatum),
                        Database.AddParameter("@from", StartTime),
                        Database.AddParameter("@until", EndTime),
                        Database.AddParameter("@stage", Convert.ToInt32(SelectedStage.ID)),
                        Database.AddParameter("@band", Convert.ToInt32(SelectedBandAdd.ID))
                        );
                }
                else
                {
                    return error;
                }
            }
            return error;
        }

        private static string CheckTime(string StartTime, string EndTime, Stage SelectedStage,DateTime SelectedDatum)
        {
            string[] strEndtime = EndTime.Split(':');
            string EndTimecom = strEndtime[0] + strEndtime[1];
            string[] strStarttime = StartTime.Split(':');
            string StartTimecom = strStarttime[0] + strStarttime[1];
            if (Convert.ToInt32(StartTimecom) >= Convert.ToInt32(EndTimecom))
            {
                return "De eindtijd moet groter zijn dan de starttijd";
            }
            else
            {
                DbDataReader reader = Database.GetData("SELECT * FROM tbl_lineup WHERE Stage=@stage AND DatePreformance=@date",
                Database.AddParameter("@stage", Convert.ToInt32(SelectedStage.ID)),
                Database.AddParameter("@date", SelectedDatum)
                );
                foreach (IDataRecord db in reader)
                {
                    string[] strStartToCheck = db["FromTime"].ToString().Split(':');
                    string[] strEndToCheck = db["UntilTime"].ToString().Split(':');
                    if (Between(Convert.ToInt32(strStartToCheck[0] + strStartToCheck[1]), Convert.ToInt32(StartTimecom), Convert.ToInt32(EndTimecom)) || Between(Convert.ToInt32(strEndToCheck[0] + strEndToCheck[1]), Convert.ToInt32(StartTimecom), Convert.ToInt32(EndTimecom)))
                    {
                        return "Deze tijden zijn al in gebruikt";
                    }
                }
            }
            return null;
        }

        private static string CheckBand(string StartTime, string EndTime, Stage SelectedStage, Band SelectedBandAdd, DateTime SelectedDatum)
        {
            string[] strEndtime = EndTime.Split(':');
            string EndTimecom = strEndtime[0] + strEndtime[1];
            string[] strStarttime = StartTime.Split(':');
            string StartTimecom = strStarttime[0] + strStarttime[1];
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_lineup WHERE Band=@band AND DatePreformance=@date",
               Database.AddParameter("@stage", Convert.ToInt32(SelectedStage.ID)),
               Database.AddParameter("@band", Convert.ToInt32(SelectedBandAdd.ID)),
               Database.AddParameter("@date", SelectedDatum)
               );
            foreach (IDataRecord db in reader)
            {
                string[] strStartToCheck = db["FromTime"].ToString().Split(':');
                string[] strEndToCheck = db["UntilTime"].ToString().Split(':');
                if (Between(Convert.ToInt32(strStartToCheck[0] + strStartToCheck[1]), Convert.ToInt32(StartTimecom), Convert.ToInt32(EndTimecom)) || Between(Convert.ToInt32(strEndToCheck[0] + strEndToCheck[1]), Convert.ToInt32(StartTimecom), Convert.ToInt32(EndTimecom)))
                {
                    return "Deze tijden voor deze band zijn al in gebruikt";
                }
            }
            return null;
        }
        public static bool Between(int num, int lower, int upper)
        {
            if (num > lower && num < upper)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public static string EditLineUp(Band SelectedBandAdd, string StartTime, string EndTime,Stage SelectedStage, DateTime SelectedDatum,string ID)
        {
            string error = "";
            error = CheckTime(StartTime, EndTime, SelectedStage,SelectedDatum);
            if (error == null)
            {
                error = CheckBand(StartTime, EndTime, SelectedStage, SelectedBandAdd,SelectedDatum);
                if (error == null)
                {
                    Database.ModifyData("UPDATE tbl_lineup SET DatePreformance=@date,FromTime=@from,UntilTime=@until,Stage=@stage,Band=@band WHERE ID=@id",
                        Database.AddParameter("@date", SelectedDatum),
                        Database.AddParameter("@from", StartTime),
                        Database.AddParameter("@until", EndTime),
                        Database.AddParameter("@stage", Convert.ToInt32(SelectedStage.ID)),
                        Database.AddParameter("@id", Convert.ToInt32(ID)),
                        Database.AddParameter("@band", Convert.ToInt32(SelectedBandAdd.ID))
                        );
                }
                else
                {
                    return error;
                }
            }
            return error;
        }

        public static void DeleteLineUp(LineUp SelectedLineUp)
        {
            Database.ModifyData("DELETE FROM tbl_lineup WHERE ID=@id",Database.AddParameter("@id",Convert.ToInt32(SelectedLineUp.ID)));
        }
        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
        public string Error
        {
            get { return null; }
        }
        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = columnName });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }
    }
}
