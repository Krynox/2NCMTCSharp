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
    public class BandDA
    {
        public static ObservableCollection<Band> GetBands()
        {
            ObservableCollection<Band> bands = new ObservableCollection<Band>();
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_bands");
            foreach (IDataRecord db in reader)
            {
                bands.Add(Create(db));
            }
            reader.Close();
            return bands;
        }

        private static Band Create(IDataRecord record)
        {
            return new Band()
            {
                ID = record["ID"].ToString().Trim(),
                Name = record["BandName"].ToString().Trim(),
                Picture = (byte[])record["Picture"],
                Description = record["Description"].ToString().Trim(),
                Twitter = record["Twitter"].ToString().Trim(),
                Genres =  GenreDA.GetBandGenres(Convert.ToInt32(record["ID"])),
                Facebook = record["Facebook"].ToString().Trim()
            };
        }
         public static Band GetBands(string p)
        {
            Band band = new Band();
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_bands WHERE ID=@id",
                Database.AddParameter("@id", Convert.ToInt32(p))
                );
            foreach (IDataRecord db in reader)
            {
                band=Create(db);
            }
            reader.Close();
            return band;
        }
    }
}