using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Stage
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public static ObservableCollection<Stage> GetStages()
        {
            ObservableCollection<Stage> stages = new ObservableCollection<Stage>();
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_stage");
            foreach (IDataRecord db in reader)
            {
                stages.Add(Create(db));
            }
            return stages;
        }
        public static Stage GetStage(string p)
        {
            Stage stage = new Stage();
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_stage WHERE ID=@id",Database.AddParameter("@id",Convert.ToInt32(p)));
            foreach (IDataRecord db in reader)
            {
                stage=(Create(db));
            }
            return stage;
        }
        private static Stage Create(IDataRecord record)
        {
            return new Stage()
            {
                ID = record["ID"].ToString(),
                Name = record["StageName"].ToString().Trim(),
            };
        }

        public static string DeleteStage(Stage SelectedStage)
        {
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_lineup WHERE Stage=@id", Database.AddParameter("@id", Convert.ToInt32(SelectedStage.ID)));
            if (reader.HasRows)
            {
                return "Gelieve eerste alle bands uit de line up van deze stage te verwijderen";
            }
            else
            {
                Database.ModifyData("DELETE FROM tbl_stage WHERE ID=@id", Database.AddParameter("@id", Convert.ToInt32(SelectedStage.ID)));
            }
            return null;
        }

        public static void AddStage(string TxtStage)
        {
            Database.ModifyData("INSERT INTO tbl_stage (StageName) VALUES (@name)",Database.AddParameter("@name",TxtStage));
        }

        public static void EditStage(string TxtStage,Stage SelectedStage)
        {
            Database.ModifyData("UPDATE tbl_stage SET StageName=@name WHERE ID=@id", Database.AddParameter("@name", TxtStage), Database.AddParameter("@id",Convert.ToInt32(SelectedStage.ID)));
        }


    }
}
