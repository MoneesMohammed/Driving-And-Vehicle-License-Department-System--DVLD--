using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsPerson
    {
        public enum enMode {AddNew = 0 , Update = 1}
        private enMode Mode = enMode.AddNew;

        public int PersonID              { get; set; }
        public string NationalNo         { get; set; }
        public string FirstName          { get; set; }
        public string SecondName         { get; set; }
        public string ThirdName          { get; set; }
        public string LastName           { get; set; }
        public DateTime DateOfBirth      { get; set; }
        public byte Gendor               { get; set; }
        public string Address            { get; set; }
        public string Phone              { get; set; }
        public string Email              { get; set; }
        public int NationalityCountryID  { get; set; }
        
        public clsCountry CountryInfo;

        private string _ImagePath;
        public string ImagePath 
        {
            get { return _ImagePath; } 
            set { _ImagePath = value; }
        }

       

        public clsPerson()
        {
            PersonID               = -1;
            NationalNo             = "";
            FirstName              = "";
            SecondName             = "";
            ThirdName              = "";
            LastName               = "";
            DateOfBirth            = DateTime.Now;
            Gendor                 = 0;
            Address                = "";
            Phone                  = "";
            Email                  = "";
            NationalityCountryID   = -1;
            ImagePath              = "";

            Mode = enMode.AddNew;

        }

        private clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, 
            string LastName ,DateTime DateOfBirth , byte Gendor ,string Address ,string Phone , string Email ,
            int NationalityCountryID , string ImagePath)
        { 
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);
            Mode = enMode.Update;

        }

        public static clsPerson Find(int PersonID)
        {
            int      NationalityCountryID = -1;
            string   NationalNo  = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor      = 0;

            if(clsPeopleDataAccess.GetPersonInfoByID(PersonID,ref NationalNo , ref FirstName ,ref SecondName ,ref ThirdName,
                ref LastName ,ref DateOfBirth ,ref Gendor ,ref Address ,ref Phone ,ref Email ,ref NationalityCountryID , ref ImagePath))
            { 
             return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,DateOfBirth, Gendor,
                                   Address, Phone, Email,NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }


        }


        public static clsPerson Find(string NationalNo)
        {
            int PersonID =-1 , NationalityCountryID = -1;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;

            if (clsPeopleDataAccess.GetPersonInfoByNationalNo(ref PersonID, NationalNo, ref FirstName, ref SecondName, ref ThirdName,
                ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor,
                                      Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }

        }


        public static string GetImagePath(int PersonID)
        {
           string ImagePath = "";

            if (clsPeopleDataAccess.GetImagePathByID(PersonID, ref ImagePath))
                return ImagePath;
            else
                return "";

        }


        private bool _AddNewPerson()
        {

            this.PersonID = clsPeopleDataAccess.AddNewPerson(NationalNo, FirstName, SecondName, ThirdName, LastName,
                                              DateOfBirth, Gendor,Address, Phone, Email, NationalityCountryID, ImagePath);
                      
            return (PersonID != -1);


        }

        private bool _UpdatePerson()
        {

            return clsPeopleDataAccess.UpdatePerson(this.PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                                              DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);

            
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                {
                        if (_AddNewPerson())
                        {
                            Mode = enMode.Update;
                            return true;

                        }
                        else
                        {
                            return false;
                        }

                }
                case enMode.Update:
                {
                        if (_UpdatePerson())
                        {

                            return true;

                        }
                        else
                        {
                            return false;
                        }

                }



            }

            return false;
        }



        public static bool DeletePerson(int PersonID)
        {
            return clsPeopleDataAccess.DeletePerson(PersonID);

        }


        public static DataTable GetAllPeople()
        {
            return clsPeopleDataAccess.GetAllPeople();

        }

        public static DataTable GetAllPeople_1()
        {
            return clsPeopleDataAccess.GetAllPeople_1();

        }


        public static bool IsPersonExists(int PersonID)
        {
            return clsPeopleDataAccess.IsPersonExists(PersonID);
        }

        public static bool IsPersonExists(string NationalNo)
        {
            return clsPeopleDataAccess.IsPersonExists(NationalNo);
        }

        public byte GetAge()
        { 
            if(PersonID == -1)
                return 0;

            return (byte)(DateTime.Now.Year - DateOfBirth.Year);

        }


    }
}
