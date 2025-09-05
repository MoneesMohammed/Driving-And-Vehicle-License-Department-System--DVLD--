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
    public class clsInternationalLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

       public int      InternationalLicenseID  { get; set; }
       public int      ApplicationID           { get; set; }
       public int      DriverID                { get; set; }
       public int      LicenseID               { get; set; } //Issued Using Local LicenseID
       public DateTime IssueDate               { get; set; }
       public DateTime ExpirationDate          { get; set; }
       public bool     IsActive                { get; set; }
       public int      CreatedByUserID         { get; set; }

        public clsApplication Application    { get; set; }
        public clsDriver      Driver         { get; set; }
        public clsLicenses    License        { get; set; }
        public clsUser        CreatedByUser  { get; set; }

        private clsApplicationType _ApplicationType = clsApplicationType.Find(6); // 6 = New International License


        public clsInternationalLicense()
        {
            InternationalLicenseID  =-1;
            ApplicationID           =-1;
            DriverID                =-1;
            LicenseID               = -1;
            IssueDate               = DateTime.Now;
            ExpirationDate          = DateTime.Now;
            IsActive                = false;
            CreatedByUserID         = -1;

            Application = new clsApplication();
            Driver = new clsDriver();
            License = new clsLicenses();
            CreatedByUser = new clsUser();



            Mode = enMode.AddNew;
        }

        private clsInternationalLicense(int InternationalLicenseID , int ApplicationID, int DriverID, int LicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        { 
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseID = LicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            Application = clsApplication.Find(ApplicationID);
            Driver = clsDriver.Find(DriverID);
            License = clsLicenses.Find(LicenseID);
            CreatedByUser = clsUser.Find(CreatedByUserID);


            Mode = enMode.Update;

        }


        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseID= -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsInternationalLicensesDataAccess.GetInternationalLicenseInfoByID(InternationalLicenseID,ref ApplicationID,ref DriverID,ref LicenseID,ref IssueDate,ref ExpirationDate,ref IsActive,ref CreatedByUserID))
            {
             return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, LicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);

            }
            else
            { 
                return null;
            }
                
        }


        public static clsInternationalLicense FindByLicenseID(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, InternationalLicenseID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsInternationalLicensesDataAccess.GetInternationalLicenseInfoByLicenseID(ref InternationalLicenseID, ref ApplicationID, ref DriverID, LicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, LicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);

            }
            else
            {
                return null;
            }

        }






        private bool CreateApplicationForInternationalLicense()
        {
            
            Application.ApplicantPersonID = License.Driver.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = _ApplicationType.ApplicationTypeID;
            Application.ApplicationStatus = 3;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = _ApplicationType.ApplicationFees;
            Application.CreatedByUserID = this.CreatedByUserID;


           return Application.Save();

        }


        private bool _AddNewInternationalLicense()
        {
            License = clsLicenses.Find(this.LicenseID);

            if (License == null)
                return false;

            if (!CreateApplicationForInternationalLicense())
                return false;

            this.ApplicationID = Application.ApplicationID ;

            this.DriverID = License.DriverID;

            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now.AddYears(1);

            this.IsActive = true;


            this.InternationalLicenseID = clsInternationalLicensesDataAccess.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.LicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            if (this.InternationalLicenseID != -1)
            {

                CreatedByUser = clsUser.Find(this.CreatedByUserID);

                return true;
            }

            return false;

        }



        private bool _UpdateInternationalLicense()
        {

            return clsInternationalLicensesDataAccess.UpdateInternationalLicense(this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.LicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

           
        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewInternationalLicense())
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
                        if (_UpdateInternationalLicense())
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






        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicensesDataAccess.GetAllInternationalLicenses();

        }

        public static DataTable GetAllInternationalLicenses_1()
        {
            return clsInternationalLicensesDataAccess.GetAllInternationalLicenses_1();

        }

        public static DataTable GetAllInternationalLicensesByPersonID(int PersonID)
        {
            return clsInternationalLicensesDataAccess.GetInternationalLicensesByPersonID(PersonID);

        }



        public bool IsNotExpired()
        {
            return (this.ExpirationDate > DateTime.Now);

        }





    }
}
