using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NewsFeed
    {
        public string ID { get; set; }
        [Required(ErrorMessage="Een bericht is vereist")]
        public string Message { get; set; }
        public DateTime EntryDate { get; set; }

        public static ObservableCollection<NewsFeed> GetNews()
        {
            ObservableCollection<NewsFeed> news = new ObservableCollection<NewsFeed>();
            DbDataReader reader = Database.GetData("SELECT * FROM tbl_news");
            foreach (IDataRecord db in reader)
            {
                news.Add(Create(db));
            }
            reader.Close();
            return news;
        }

        private static NewsFeed Create(IDataRecord db)
        {
            return new NewsFeed()
            {
                ID = db["ID"].ToString().Trim(),
                Message = db["News"].ToString().Trim(),
                EntryDate=Convert.ToDateTime(db["EntryDate"])
            };
        }

        public static void DeleteNews(string id)
        {
            Database.ModifyData("DELETE FROM tbl_news WHERE id=@id",Database.AddParameter("@id",Convert.ToInt32(id)));
        }

        public static void CreateNews(NewsFeed mess)
        {
            Database.ModifyData("INSERT INTO tbl_news (News,EntryDate) VALUES (@news,@date)",Database.AddParameter("@news",mess.Message), Database.AddParameter("@date",DateTime.Now));
        }

    }
}
