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
    public class Genre : IDataErrorInfo
    {
        public string ID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
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
                GenreName = record["Genre"].ToString().Trim(),
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

        public static string DeleteGenre(Genre SelectedGenre)
        {
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_genresBands WHERE GenreID=@id",
               Database.AddParameter("@id", Convert.ToInt32(SelectedGenre.ID))
               );
            if (reader.HasRows)
            {
                return "Gelieve eerste de genre uit de alle bands te halen!";
            }
            else
            {
                Database.ModifyData("DELETE FROM tbl_genres WHERE ID=@id",
                   Database.AddParameter("@id", Convert.ToInt32(SelectedGenre.ID))
                   );
            }
            return null;
        }

        public static void SaveGenre(string EditGenre)
        {
            Database.ModifyData("INSERT INTO tbl_genres (Genre) VALUES (@name)",
                Database.AddParameter("@name",EditGenre)
                );
        }

        public static void EditGenre(string EditGenre, string p)
        {
            Database.ModifyData("UPDATE tbl_genres SET Genre=@name WHERE Genre=@lastname",
                Database.AddParameter("@name", EditGenre),
                Database.AddParameter("@lastname", p)
                );
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
