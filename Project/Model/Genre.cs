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
    class Genre
    {
        public string ID { get; set; }
        public string GenreName { get; set; }
        public static ObservableCollection<Genre> GetGenres()
        {
            ObservableCollection<Genre> contacts = new ObservableCollection<Genre>();
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_genres");
            foreach (IDataRecord db in reader)
            {
                contacts.Add(Create(db));
            }
            return contacts;
        }

        private static Genre Create(IDataRecord record)
        {
            return new Genre()
            {
                ID = record["ID"].ToString(),
                GenreName = record["Genre"].ToString(),
            };
        }

        public static ObservableCollection<Genre> GetBandGenres(int p)
        {
            ObservableCollection<Genre> genres = new ObservableCollection<Genre>();
            DbDataReader reader = Database.GetData("SELECT tbl_genres.ID,Genre FROM tbl_genres,tbl_genresBands,tbl_bands WHERE tbl_genres.ID=tbl_genresBands.GenreID AND tbl_bands.ID=tbl_genresBands.BandID AND tbl_bands.ID=@id",
                Database.AddParameter("@id",p)
                );
            foreach (IDataRecord db in reader)
            {
                genres.Add(Create(db));
            }
            return genres;
        }
    }
}
