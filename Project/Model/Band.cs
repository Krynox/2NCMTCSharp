using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Project
{
    class Band
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public string Description { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public ObservableCollection<Genre> Genres{ get; set; }

        public Band()
        {
            Genres = new ObservableCollection<Genre>();
        }
        public static byte[] GetPhoto(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, (int)fs.Length);
            fs.Close();
            return data;
        }
        public static void CreateBand(Band FormBand, string ImagePath)
        {
            int last = 0;
            int test=Database.ModifyData("INSERT INTO tbl_bands (BandName,Picture,Description,Twitter,Facebook) VALUES (@BandName,@Picture,@Desc,@twitter,@facebook)",
                Database.AddParameter("@BandName",FormBand.Name),
                Database.AddParameter("@Picture",GetPhoto(ImagePath)),
                Database.AddParameter("@Desc",FormBand.Description),
                Database.AddParameter("@twitter",FormBand.Twitter),
                Database.AddParameter("@facebook",FormBand.Facebook)
                );

            DbDataReader reader = Database.GetData("SELECT MAX(ID) as grootste FROM tbl_bands");
            foreach (IDataRecord db in reader)
            {
                last = Convert.ToInt32(db["grootste"]);
            }
            foreach(Genre g in FormBand.Genres)
            {
                Database.ModifyData("INSERT INTO tbl_genresBands (GenreID,BandID) VALUES(@GenreID,@BandID)",
                    Database.AddParameter("@GenreID",g.ID),
                    Database.AddParameter("@BandID",last)
                );
            }

        }

        public static ObservableCollection<Band> GetBands()
        {
            ObservableCollection<Band> bands = new ObservableCollection<Band>();
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_bands");
            foreach (IDataRecord db in reader)
            {
                bands.Add(Create(db));
            }
            return bands;
        }

        private static Band Create(IDataRecord record)
        {
            return new Band()
            {
                ID = record["ID"].ToString(),
                Name = record["BandName"].ToString(),
                Picture=(byte[])record["Picture"],
                Description = record["Description"].ToString(),
                Twitter=record["Twitter"].ToString(),
                Genres=Genre.GetBandGenres(Convert.ToInt32(record["ID"])),
                Facebook=record["Facebook"].ToString()
            };
        }
    }
}
