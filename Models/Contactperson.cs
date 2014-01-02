using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Data.Common;
using Project.Model;
using System.Data;
using System.Configuration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project
{
    public class Contactperson :IDataErrorInfo
    {


        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Company { get; set; }

        [Required]
        public ContactpersonType JobRole { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        //[RegularExpression(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"]
        [StringLength(50, MinimumLength = 3)]
        public string Email  { get; set; }

        [Required]
        //[RegularExpression(@"\+3[23](?:\s*?\(0\))?(?:\s*?\d){8}$")]
        [StringLength(20, MinimumLength = 3)]
        public string Phone { get; set; }

        [Required]
        //[RegularExpression(@"\+3[23](?:\s*?\(0\))?(?:\s*?\d){9}$"]
        [StringLength(20, MinimumLength = 3)]
        public string CellPhone { get; set; }

        public string ID { get; set; }

        public static ObservableCollection<Contactperson> GetContactPersons()
        {
            ObservableCollection<Contactperson> contacts = new ObservableCollection<Contactperson>();
            DbDataReader reader = Database.GetData("SELECT tbl_contacts.ID as ContactID,ContactName,Company,City,Email,Phone,CellPhone,tbl_contactType.ID as TypeID,tbl_contactType.JobRole FROM tbl_contacts, tbl_contactType WHERE tbl_contacts.JobRole=tbl_contactType.ID");
            foreach (IDataRecord db in reader)
            {
                contacts.Add(Create(db));
            }
            return contacts;
        }

        private static Contactperson Create(IDataRecord record)
        {
            return new Contactperson()
            {
                ID = record["ContactID"].ToString().Trim(),
                Name = record["ContactName"].ToString().Trim(),
                Company = record["Company"].ToString().Trim(),
                JobRole = new ContactpersonType { ID=Convert.ToInt32(record["TypeID"]),Name=record["JobRole"].ToString()},
                City = record["City"].ToString().Trim(),
                Email = record["Email"].ToString().Trim(),
                Phone = record["Phone"].ToString().Trim(),
                CellPhone = record["CellPhone"].ToString().Trim()
            };
        }
        public static void AddContact(string _name,string _company,ContactpersonType _jobRole,string _city,string _email,string _phone,string _cellphone)
        {
            Database.ModifyData("INSERT INTO tbl_contacts (ContactName,Company,JobRole,City,Email,Phone,CellPhone) VALUES(@name,@company,@jobrole,@city,@email,@phone,@cellphone)", 
                Database.AddParameter("@name",_name),
                Database.AddParameter("@company",_company),
                Database.AddParameter("@jobrole",Convert.ToInt32(_jobRole.ID)),
                Database.AddParameter("@city",_city),
                Database.AddParameter("@email",_email),
                Database.AddParameter("@phone",_phone),
                Database.AddParameter("@cellphone",_cellphone)
                );
        }
        public static void EditContact(string _name, string _company, ContactpersonType _jobRole, string _city, string _email, string _phone, string _cellphone,string _id)
        {
            Database.ModifyData("UPDATE tbl_contacts SET ContactName=@name,Company=@company,JobRole=@jobrole,City=@city,Email=@email,Phone=@phone,CellPhone=@cellphone WHERE ID=@id",
                Database.AddParameter("@id",Convert.ToInt32(_id)),
                Database.AddParameter("@name", _name),
                Database.AddParameter("@company", _company),
                Database.AddParameter("@jobrole", Convert.ToInt32(_jobRole.ID)),
                Database.AddParameter("@city", _city),
                Database.AddParameter("@email", _email),
                Database.AddParameter("@phone", _phone),
                Database.AddParameter("@cellphone", _cellphone)
                );
        }
        public static void DeleteContact(Contactperson s)
        {
            Database.ModifyData("DELETE FROM tbl_contacts WHERE ID=@id", Database.AddParameter("@id", s.ID));
        }
        public static ObservableCollection<Contactperson> ZoekContact(string _contactName, string _contactCompany, ContactpersonType _contactJobRole)
        {
            string jobrole = "";
            if (_contactJobRole != null)
            {
                jobrole = _contactJobRole.Name;
            }
            else {
                jobrole = "";
            }
            ObservableCollection<Contactperson> contacts = new ObservableCollection<Contactperson>();
            DbDataReader reader = Database.GetData("SELECT tbl_contacts.ID as ContactID,ContactName,Company,City,Email,Phone,CellPhone,tbl_contactType.ID as TypeID,tbl_contactType.JobRole FROM tbl_contacts, tbl_contactType WHERE tbl_contacts.JobRole=tbl_contactType.ID and ContactName LIKE @name AND Company LIKE @company AND tbl_contactType.JobRole LIKE @jobrole",
                Database.AddParameter("@name","%"+ _contactName +"%"),
                Database.AddParameter("@company","%"+ _contactCompany +"%"),
                Database.AddParameter("@jobrole","%"+ jobrole +"%")
                );
            foreach (IDataRecord db in reader)
            {
                contacts.Add(Create(db));
            }
            return contacts;
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
