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
                ID = record["ID"].ToString().Trim(),
                Name = record["BandName"].ToString().Trim(),
                Picture=(byte[])record["Picture"],
                Description = record["Description"].ToString().Trim(),
                Twitter = record["Twitter"].ToString().Trim(),
                Genres=Genre.GetBandGenres(Convert.ToInt32(record["ID"])),
                Facebook = record["Facebook"].ToString().Trim()
            };
        }

        public static string DeleteBand(Band SelectedBand)
        {
            DbDataReader reader=Database.GetData("SELECT * FROM tbl_bands,tbl_lineup WHERE tbl_bands.ID=tbl_lineup.Band AND tbl_bands.ID=@id",
                Database.AddParameter("@id",Convert.ToInt32(SelectedBand.ID))
                );
            if (reader.HasRows)
            {
                return "Gelieve eerste de band uit de linup te halen!";
            }
            else
            {
                Database.ModifyData("DELETE FROM tbl_genresBands WHERE BandID=@id",
                   Database.AddParameter("@id", Convert.ToInt32(SelectedBand.ID))
                   );
                Database.ModifyData("DELETE FROM tbl_bands WHERE ID=@id", Database.AddParameter("@id", Convert.ToInt32(SelectedBand.ID)));

            }
            return null;
        }
        public static void EditBand(Band SelectedBand, string ImagePath)
        {
            if (ImagePath != null)
            {
                Database.ModifyData("UPDATE tbl_bands SET BandName=@name,Description=@desc,Twitter=@twitter,Facebook=@facebook,Picture,@pic",
                    Database.AddParameter("@name",SelectedBand.Name),
                    Database.AddParameter("@desc",SelectedBand.Description),
                    Database.AddParameter("@twitter",SelectedBand.Twitter),
                    Database.AddParameter("@facebook",SelectedBand.Facebook),
                    Database.AddParameter("@pic",GetPhoto(ImagePath))
                 );
            }
            else {
                Database.ModifyData("UPDATE tbl_bands SET BandName=@name,Description=@desc,Twitter=@twitter,Facebook=@facebook",
                    Database.AddParameter("@name", SelectedBand.Name),
                    Database.AddParameter("@desc", SelectedBand.Description),
                    Database.AddParameter("@twitter", SelectedBand.Twitter),
                    Database.AddParameter("@facebook", SelectedBand.Facebook)
                 );
            }
            Database.ModifyData("DELETE FROM tbl_genresBands WHERE BandID=@id",
            Database.AddParameter("@id", SelectedBand.ID)
            );
            foreach (Genre g in SelectedBand.Genres)
            {
                Database.ModifyData("INSERT INTO tbl_genresBands (GenreID,BandID) VALUES(@GenreID,@BandID)",
                    Database.AddParameter("@GenreID", g.ID),
                    Database.AddParameter("@BandID", SelectedBand.ID)
                );
            }
        }

        public static ObservableCollection<Band> GetBandsSearch(string ZoekBands)
        {
            ObservableCollection<Band> bands = new ObservableCollection<Band>();
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_bands WHERE BandName like @name",
                Database.AddParameter("@name","%"+ZoekBands+"%")
                );
            foreach (IDataRecord db in reader)
            {
                bands.Add(Create(db));
            }
            return bands;
        }
    }
}
