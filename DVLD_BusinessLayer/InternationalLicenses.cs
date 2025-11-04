using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_BusinessLayer
{
    public class clsInternationalLicense : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

       public int      InternationalLicenseID      { get; set; }
       public int      DriverID                    { get; set; }
       public int      IssuedUsingLocalLicenseID   { get; set; } //Issued Using Local LicenseID
       public DateTime IssueDate                   { get; set; }
       public DateTime ExpirationDate              { get; set; }
       public bool     IsActive                    { get; set; }

       public clsDriver DriverInfo;
        


        public clsInternationalLicense()
        {
            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;

            InternationalLicenseID  =-1;
            DriverID                =-1;
            IssuedUsingLocalLicenseID   = -1;
            IssueDate               = DateTime.Now;
            ExpirationDate          = DateTime.Now;
            IsActive                = true;
           
            Mode = enMode.AddNew;
        }

        private clsInternationalLicense(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
               enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID ,
               int InternationalLicenseID , int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive )
        {
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.InternationalLicenseID = InternationalLicenseID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;

           
            DriverInfo = clsDriver.FindByDriverID(DriverID);
           
            Mode = enMode.Update;

        }


        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseID= -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            bool IsFound = clsInternationalLicensesDataAccess.GetInternationalLicenseInfoByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref LicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID);

            if (IsFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsInternationalLicense(ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, InternationalLicenseID, DriverID, LicenseID, IssueDate, ExpirationDate, IsActive);
            }
            else
            { 
                return null;
            }
                
        }


        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicensesDataAccess.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return (this.InternationalLicenseID != -1);
           
        }

        private bool _UpdateInternationalLicense()
        {

            return clsInternationalLicensesDataAccess.UpdateInternationalLicense(this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

           
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    
                   if (_AddNewInternationalLicense())
                   {
                       Mode = enMode.Update;
                       return true;

                   }
                   else
                   {
                       return false;
                   }

                case enMode.Update:
                    
                    return _UpdateInternationalLicense();
                        
            }

            return false;
        }


        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicensesDataAccess.GetAllInternationalLicenses();

        }

       
        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicensesDataAccess.GetDriverInternationalLicenses(DriverID);
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return clsInternationalLicensesDataAccess.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }


    }
}
