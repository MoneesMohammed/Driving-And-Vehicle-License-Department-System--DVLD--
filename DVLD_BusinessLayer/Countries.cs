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

        public int CountryID { get; set; }

        public string CountryName { get; set; }
       

        public clsCountry()
        {
            CountryID = -1;
            CountryName = "";
           
        }


        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        
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

        public static DataTable GetAllCountries()
        {
            return clsCountriesDataAccess.GetAllCountries();

        }

        

    }
}
