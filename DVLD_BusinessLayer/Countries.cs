using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayar;

namespace DVLD_BusinessLayer
{
    public class clsCountry
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;


        public int CountryID { get; set; }

        public string CountryName { get; set; }
       

        public clsCountry()
        {
            CountryID = -1;
            CountryName = "";
           
            Mode = enMode.AddNew;
        }


        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        

            Mode = enMode.Update;


        }


        public static clsCountry Find(int ID)
        {

            string CountryName = "";

            if (clsCountriesDataAccess.GetCountryInfoByID(ID, ref CountryName))
                return new clsCountry(ID, CountryName);
            else
                return null;
        }



        public static clsCountry Find(string CountryName)
        {
            int ID = -1;
            

            if (clsCountriesDataAccess.GetCountryInfoByName(ref ID, CountryName))
                return new clsCountry(ID, CountryName);
            else
                return null;


        }

        public static bool IsCountryExistsByCountryName(string CountryName)
        {
            return clsCountriesDataAccess.IsCountryExistsByCountryName(CountryName);


        }


        private bool _AddNewCountry()
        {

            this.CountryID = clsCountriesDataAccess.AddNewCountry(this.CountryName);
            return (CountryID != -1);


        }

        private bool _UpdateCountry()
        {

            return clsCountriesDataAccess.UpdateCountry(CountryID, CountryName);


        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewCountry())
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
                        if (_UpdateCountry())
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



        public static bool DeleteCountry(int ID)
        {
            return clsCountriesDataAccess.DeleteCountry(ID);

        }


        public static DataTable GetAllCountries()
        {
            return clsCountriesDataAccess.GetAllCountries();

        }

        public static bool IsCountryExists(int ID)
        {
            return clsCountriesDataAccess.IsCountryExists(ID);
        }

    }
}
