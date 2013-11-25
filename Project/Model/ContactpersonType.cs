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
    class ContactpersonType
    {
        public int ID { get; set; }
        public string Name { get; set; }


        public static ObservableCollection<ContactpersonType> GetTypes()
        {
            ObservableCollection<ContactpersonType> contacts = new ObservableCollection<ContactpersonType>();
            DbDataReader reader = Database.GetData("SELECT JobRole,ID FROM tbl_contactType");
            foreach (IDataRecord db in reader)
            {
                contacts.Add(Create(db));
            }
            return contacts;
        }

        private static ContactpersonType Create(IDataRecord db)
        {
            return new ContactpersonType()
            {
                ID=Convert.ToInt32(db["ID"]),
                Name = db["JobRole"].ToString()
            };
        }
        public static void AddContact(string _contacttypename)
        { 
            Database.ModifyData("INSERT INTO tbl_contactType (JobRole) VALUES(@name)",Database.AddParameter("@name",_contacttypename));
        }
        public static void EditType(ContactpersonType s,string _name)
        {
            Database.ModifyData("UPDATE tbl_contactType SET JobRole=@name WHERE ID=@id", Database.AddParameter("@id", Convert.ToInt32(s.ID)), Database.AddParameter("@name", _name));
        }
        public static string DeleteFunctie(ContactpersonType s)
        { 
           DbDataReader reader= Database.GetData("SELECT * FROM tbl_contacts,tbl_contactType WHERE tbl_contactType.ID=tbl_contacts.JobRole AND tbl_contactType.JobRole=@name",Database.AddParameter("@name",s.Name));
           if (reader.HasRows)
           {
               return "Gelieve eerste alle Contactpersonen met deze functie te verwijderen";
           }
           else
           {
               Database.ModifyData("DELETE FROM tbl_contactType WHERE ID=@id",Database.AddParameter("@id",Convert.ToInt32(s.ID)));
           }
           return null;

        }
    }
}
