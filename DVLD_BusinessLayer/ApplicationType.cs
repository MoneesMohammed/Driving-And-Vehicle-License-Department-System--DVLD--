using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }


        public clsApplicationType()
        { 
            
            ApplicationTypeID    = -1;
            ApplicationTypeTitle = "";
            ApplicationFees      = 0;

            Mode = enMode.AddNew;
        }

        private clsApplicationType(int ApplicationTypeID,string ApplicationTypeTitle, decimal ApplicationFees)
        {
            this.ApplicationTypeID    = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees      = ApplicationFees;

            Mode = enMode.Update;

        }

        public static clsApplicationType Find(int ApplicationTypeID)
        {
            string ApplicationTypeTitle ="";
            decimal ApplicationFees=0;

            if (clsApplicationTypeDataAccess.GetApplicationTypeInfoByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            else
                return null;

        }

        private bool _AddNewApplicationType()
        {
            this.ApplicationTypeID = clsApplicationTypeDataAccess.AddNewApplicationType(this.ApplicationTypeTitle, this.ApplicationFees);
            return (ApplicationTypeID != -1);
        }


        private bool _UpdateApplicationType()
        {
          return clsApplicationTypeDataAccess.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewApplicationType())
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
                        if (_UpdateApplicationType())
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


        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeDataAccess.GetAllApplicationTypes();

        }

        public static DataTable GetAllApplicationTypes_1()
        {
            return clsApplicationTypeDataAccess.GetAllApplicationTypes_1();

        }




    }
}
