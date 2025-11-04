using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using static DVLD_BusinessLayer.clsLicense;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_BusinessLayer
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

        public enum enIssueReason { FirstTime = 1, Renew = 2 , ReplacementForDamaged = 3, ReplacementForLost = 4};

        public int      LicenseID        { get; set; }
        public int      ApplicationID    { get; set; }
        
        public int      DriverID         { get; set; }
        public int      LicenseClass     { get; set; }
        public DateTime IssueDate        { get; set; }
        public DateTime ExpirationDate   { get; set; }
        public string   Notes            { get; set; }
        public decimal PaidFees         { get; set; }
        public bool     IsActive         { get; set; } 
        public enIssueReason IssueReason      { get; set; }
        public int     CreatedByUserID  { get; set; }

       
        public clsLicenseClass LicenseClassInfo;

        public clsDriver DriverInfo;

        public clsDetainedLicense DetainedInfo;

        public bool IsDetained
        {
            get
            {
                return clsDetainedLicense.IsLicenseDetained(this.LicenseID);

            }
        }

        public string IssueReasonText
        {
            get 
            {
                return GetIssueReasonText(this.IssueReason);
            }
        
        }

       

        public clsLicense()
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


            Mode = enMode.AddNew;
        }


        private clsLicense(int LicenseID,int ApplicationID,int DriverID,int LicenseClass,DateTime IssueDate,DateTime ExpirationDate,string Notes, decimal PaidFees,bool IsActive, enIssueReason IssueReason,int CreatedByUserID)
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

            LicenseClassInfo = clsLicenseClass.Find(LicenseClass);
            DriverInfo = clsDriver.FindByDriverID(DriverID);
            DetainedInfo = clsDetainedLicense.FindByLicenseID(LicenseID);

            Mode = enMode.Update;
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now , ExpirationDate = DateTime.Now;
            string Notes  = "";
            decimal PaidFees  = -1;
            bool IsActive  = false;
            byte IssueReason  = 0 ;

            if (clsLicenseDataAccess.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;
        }

        public static clsLicense FindByApplicationID(int ApplicationID)
        {
            int LicenseID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = -1;
            bool IsActive = false;
            byte IssueReason = 0;

            if (clsLicenseDataAccess.GetLicenseInfoByApplicationID(ref LicenseID, ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;
        }


        private bool _AddNewLicense()
        {
            
            this.LicenseID = clsLicenseDataAccess.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {

            return clsLicenseDataAccess.UpdateLicense(this.LicenseID,this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
           

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

        public static bool IsLicenseExistsByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseDataAccess.IsLicenseExistsByPersonID(PersonID, LicenseClassID);

        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicenseDataAccess.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }


        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseDataAccess.GetDriverLicenses(DriverID);
        }


        public bool DeactiveCurrentLicense()
        {
            return clsLicenseDataAccess.DeactiveLicense(this.LicenseID);

        }


        private string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";

                case enIssueReason.Renew:
                    return "Renew";

                case enIssueReason.ReplacementForDamaged:
                    return "Replacement For Damaged";
                case enIssueReason.ReplacementForLost:
                    return "Replacement For Lost";
                default:
                    break;
            }

            return "";

        }


        public int Detain(decimal FineFees , int CreatedByUserID)
        {
            clsDetainedLicense DetainedLicense = new clsDetainedLicense();

            DetainedLicense.LicenseID = this.LicenseID;
            DetainedLicense.DetainDate = DateTime.Now;
            DetainedLicense.FineFees = FineFees;
            DetainedLicense.CreatedByUserID = CreatedByUserID;

            if (!DetainedLicense.Save())
            {
                return -1;
            }

            return DetainedLicense.DetainID;

        }


        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            Application.CreatedByUserID = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = - 1;
                return false;
            }

            ApplicationID = Application.ApplicationID;
            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, Application.ApplicationID);

        }


        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {

                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID    = Application.ApplicationID;
            NewLicense.DriverID         = this.DriverID;
            NewLicense.LicenseClass     = this.LicenseClass;
            NewLicense.IssueDate        = DateTime.Now;
            NewLicense.ExpirationDate   = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            NewLicense.Notes            = Notes;
            NewLicense.PaidFees         = this.LicenseClassInfo.ClassFees;
            NewLicense.IsActive         = true;
            NewLicense.IssueReason      = enIssueReason.Renew;
            NewLicense.CreatedByUserID  = CreatedByUserID;

            if(!NewLicense.Save())
                { return null; }


            DeactiveCurrentLicense();

            return NewLicense;
        }


        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {
            clsApplication.enApplicationType ApplicationTypeID;
            ApplicationTypeID = (IssueReason == enIssueReason.ReplacementForLost) ? clsApplication.enApplicationType.ReplacementLostDrivingLicense : clsApplication.enApplicationType.ReplacementDamagedDrivingLicense;

            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate   = DateTime.Now;
            Application.ApplicationTypeID = (int)ApplicationTypeID;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate    = DateTime.Now;
            Application.PaidFees          = clsApplicationType.Find((int)ApplicationTypeID).ApplicationFees;
            Application.CreatedByUserID   = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (!NewLicense.Save())
            { return null; }


            DeactiveCurrentLicense();

            return NewLicense;


        }



    }



}
