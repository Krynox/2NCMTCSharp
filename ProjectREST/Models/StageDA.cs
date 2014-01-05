using ModelPort;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ProjectREST.Models
{
    public class StageDA
    {
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
        private static Stage Create(IDataRecord record)
        {
            return new Stage()
            {
                ID = record["ID"].ToString(),
                Name = record["StageName"].ToString().Trim(),
            };
        }
        public static Stage GetStage(string id)
        {
            Stage stages = new Stage();
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_stage WHERE ID=@id",Database.AddParameter("@id",id));
            foreach (IDataRecord db in reader)
            {
                stages=Create(db);
            }
            return stages;
        }
    }
}