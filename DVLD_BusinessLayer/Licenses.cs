using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_BusinessLayer
{
    public class clsLicenses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;


        public int      LicenseID        { get; set; }
        public int      ApplicationID    { get; set; }
        
        public int      DriverID         { get; set; }
        public int      LicenseClass     { get; set; }
        public DateTime IssueDate        { get; set; }
        public DateTime ExpirationDate   { get; set; }
        public string   Notes            { get; set; }
        public decimal PaidFees         { get; set; }
        public bool     IsActive         { get; set; } 
        public byte     IssueReason      { get; set; }
        public int     CreatedByUserID  { get; set; }

        public clsApplication Application { get; set; }
        public clsUser CreatedByUser { get; set; }

        public clsLicenseClass LicenseClassInfo { get; set; }

        public clsDriver Driver { get; set; }

        public clsLicenses()
        {

            LicenseID       = -1;
            ApplicationID   = -1;
            DriverID        = -1;
            LicenseClass    = -1;
            IssueDate       = DateTime.MinValue;
            ExpirationDate  = DateTime.MinValue;
            Notes           = "";
            PaidFees        = 0;
            IsActive        = false;
            IssueReason     = 0;
            CreatedByUserID = -1;

            Application = new clsApplication();
            LicenseClassInfo = new clsLicenseClass();
            CreatedByUser = new clsUser();
            Driver = new clsDriver();

            Mode = enMode.AddNew;
        }


        private clsLicenses(int LicenseID,int ApplicationID,int DriverID,int LicenseClass,DateTime IssueDate,DateTime ExpirationDate,string Notes, decimal PaidFees,bool IsActive,byte IssueReason,int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            Application = clsApplication.Find(ApplicationID);
            LicenseClassInfo = clsLicenseClass.Find(LicenseClass);
            CreatedByUser = clsUser.Find(CreatedByUserID);
            Driver = clsDriver.Find(DriverID);

            Mode = enMode.Update;
        }

        public static clsLicenses Find(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now , ExpirationDate = DateTime.Now;
            string Notes  = "";
            decimal PaidFees  = -1;
            bool IsActive  = false;
            byte IssueReason  = 0 ;

            if (clsLicenseDataAccess.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
                return new clsLicenses(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            else
                return null;
        }

        public static clsLicenses FindByApplicationID(int ApplicationID)
        {
            int LicenseID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = -1;
            bool IsActive = false;
            byte IssueReason = 0;

            if (clsLicenseDataAccess.GetLicenseInfoByApplicationID(ref LicenseID, ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
                return new clsLicenses(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            else
                return null;
        }


        private bool _AddNewLicense()
        {
            Application = clsApplication.Find(this.ApplicationID);

            int PersonID = Application.ApplicantPersonID;

            if (clsDriver.IsDriverExists(PersonID))
            {
                Driver = clsDriver.FindByPersonID(PersonID);
                this.DriverID = Driver.DriverID;
            }
            else
            {
                Driver.PersonID = PersonID;
                Driver.CreatedByUserID = this.CreatedByUserID;
                Driver.CreatedDate = DateTime.Now;

                if (Driver.Save())
                {
                    this.DriverID = Driver.DriverID;
                }

            }


            LicenseClassInfo = clsLicenseClass.Find(this.LicenseClass);

            this.ExpirationDate = DateTime.Now.AddYears(LicenseClassInfo.DefaultValidityLength);
            this.IssueDate = DateTime.Now;
            this.PaidFees = LicenseClassInfo.ClassFees;

            this.IsActive = true;
            // this.IssueReason = Convert.ToByte(Application.ApplicationTypeID);
            //-_- IssueReason -_-  // 3 = Damaged License // 4 = Lost License
            if (Application.ApplicationTypeID == 3 || Application.ApplicationTypeID == 4)
            {
                int ID = Application.ApplicationTypeID;

                this.IssueReason = Convert.ToByte((ID == 3) ? 4 : 3);
            }
            else
            {
                this.IssueReason = Convert.ToByte(Application.ApplicationTypeID);
            }



            this.LicenseID = clsLicenseDataAccess.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);

            if (this.LicenseID != -1)
            {
                
                CreatedByUser = clsUser.Find(this.CreatedByUserID);

                return true;
            }

            return false;
        }

        private bool _UpdateLicense()
        {

            return clsLicenseDataAccess.UpdateLicense(this.LicenseID,this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
           

        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewLicense())
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
                        if (_UpdateLicense())
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


        public static DataTable GetAllLicenses()
        {
            return clsLicenseDataAccess.GetAllLicenses();

        }

        private int _CountIssueReason(int DriverID , int LicenseClass)
        {
            return clsLicenseDataAccess.CountIssueReason( DriverID, LicenseClass);
        }

        public static DataTable GetAllLicenseByPersonID(int PersonID)
        {
            return clsLicenseDataAccess.GetAllLicenseByPersonID(PersonID);
        }

        public bool IsNotExpired()
        {
          return (this.ExpirationDate > DateTime.Now);
            
        }



    }



}
