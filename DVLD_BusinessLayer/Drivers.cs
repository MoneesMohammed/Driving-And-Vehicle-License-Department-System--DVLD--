using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_BusinessLayer
{
    public class clsDriver
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

       public int DriverID { get; set; }
       public int PersonID { get; set; }
       public int CreatedByUserID { get; set; }
       public DateTime CreatedDate { get; set; }

        public clsPerson clsPerson { get; set; }
        public clsUser CreatedByUser { get; set; }

        public clsDriver()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;

            clsPerson     = new clsPerson();
            CreatedByUser = new clsUser();

            Mode = enMode.AddNew;
        }

        private clsDriver(int DriverID,int PersonID, int CreatedByUserID, DateTime CreatedDate)
        { 
            this.DriverID        = DriverID;
            this.PersonID        = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate     = CreatedDate;

            clsPerson = clsPerson.Find(PersonID);
            CreatedByUser = clsUser.Find(CreatedByUserID);

            Mode = enMode.Update;
        }




        public static clsDriver Find(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            

            if (clsDriverDataAccess.GetDriverInfoByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;


            if (clsDriverDataAccess.GetDriverInfoByPersonID(ref DriverID, PersonID, ref CreatedByUserID, ref CreatedDate))
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }


        private bool _AddNewDriver()
        {
            
            this.DriverID = clsDriverDataAccess.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            if (this.DriverID != -1)
            {
                clsPerson = clsPerson.Find(PersonID);
                CreatedByUser = clsUser.Find(CreatedByUserID);

                return true;
            }

            return false;
        }

        private bool _UpdateDriver()
        {

            return clsDriverDataAccess.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);


        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewDriver())
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
                        if (_UpdateDriver())
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


        public static DataTable GetAllDriver()
        {
            return clsDriverDataAccess.GetAllDrivers();

        }

        public static DataTable GetAllDriver_1()
        {
            return clsDriverDataAccess.GetAllDrivers_1();

        }


        public static bool IsDriverExists(int PersonID)
        {
            return clsDriverDataAccess.IsDriverExists(PersonID);
        }

        


    }

}
