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
    }
}
