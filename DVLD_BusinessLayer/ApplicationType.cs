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
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }


        public clsApplicationType()
        { 
            
            ApplicationTypeID    = -1;
            ApplicationTypeTitle = "";
            ApplicationFees      = 0;
        }

        private clsApplicationType(int ApplicationTypeID,string ApplicationTypeTitle, decimal ApplicationFees)
        {
            this.ApplicationTypeID    = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees      = ApplicationFees;

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


        private bool _UpdateApplicationType()
        {

          return clsApplicationTypeDataAccess.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);


        }

        public bool Save()
        {
          if (_UpdateApplicationType())
          {

              return true;

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
