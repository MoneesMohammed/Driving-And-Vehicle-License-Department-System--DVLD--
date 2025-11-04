using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_BusinessLayer
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        protected enMode Mode = enMode.AddNew;

        public enum enApplicationType { NewDrivingLicense = 1 , RenewDrivingLicense =2 , ReplacementLostDrivingLicense = 3 ,
                                        ReplacementDamagedDrivingLicense = 4 , ReleaseDetainedDrivingLicsense = 5,
                                        NewInternationalLicense =6 , RetakeTest = 7 };

        public enum enApplicationStatus { New = 1 , Cancelled = 2 , Completed = 3};

        public int                 ApplicationID     {  get; set; }
        public int                 ApplicantPersonID { get; set; }
        
        public DateTime            ApplicationDate   { get; set; }
        public int                 ApplicationTypeID { get; set; }
        public clsApplicationType  ApplicationTypeInfo;
        public enApplicationStatus ApplicationStatus { get; set; }

        public string StatusText
        {
            get 
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";

                }
                  
            }
        
        }

        public string ApplicantFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }
        }

        public DateTime            LastStatusDate    { get; set; }
        public decimal             PaidFees          { get; set; }
        public int                 CreatedByUserID   { get; set; }
        public clsUser             CreatedByUserInfo;


        public clsApplication()
        {
            ApplicationID = -1;
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.MinValue;
            ApplicationTypeID = -1;
            ApplicationStatus = enApplicationStatus.New;
            LastStatusDate = DateTime.MinValue;
            PaidFees = 0;
            CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }

        private clsApplication(int ApplicationID,int ApplicantPersonID,DateTime ApplicationDate,enApplicationType ApplicationTypeID,enApplicationStatus ApplicationStatus,DateTime LastStatusDate,decimal PaidFees,int CreatedByUserID)
        {
            this.ApplicationID     = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate   = ApplicationDate;
            this.ApplicationTypeID = (int)ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate    = LastStatusDate;
            this.PaidFees          = PaidFees;
            this.CreatedByUserID   = CreatedByUserID;

           
            ApplicationTypeInfo = clsApplicationType.Find((int)ApplicationTypeID);
            CreatedByUserInfo   = clsUser.FindByUserID(CreatedByUserID);

            Mode = enMode.Update;
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1, ApplicationTypeID = -1, CreatedByUserID = -1;
            DateTime ApplicationDate = DateTime.MinValue, LastStatusDate = DateTime.MinValue;
            decimal PaidFees = 0;
            byte ApplicationStatus = 0;

            if (clsApplicationsDataAccess.GetApplicationInfoByID(ApplicationID,ref ApplicantPersonID,ref ApplicationDate,ref ApplicationTypeID,ref ApplicationStatus,ref LastStatusDate,ref PaidFees,ref CreatedByUserID))
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, (enApplicationType)ApplicationTypeID,(enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            else
                return null;
        }

        


        private bool _AddNewApplication()
        {

            this.ApplicationID = clsApplicationsDataAccess.AddNewApplication(this.ApplicantPersonID,this.ApplicationDate ,this.ApplicationTypeID, (byte)this.ApplicationStatus,this.LastStatusDate,this.PaidFees,this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {

            return clsApplicationsDataAccess.UpdateApplication(this.ApplicationID,this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);


        }

        public bool Cancel()
        {
          return clsApplicationsDataAccess.UpdateStatus(this.ApplicationID,2);
        }

        public bool SetComplete()
        {
          return clsApplicationsDataAccess.UpdateStatus(this.ApplicationID, 3);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewApplication())
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
                        if (_UpdateApplication())
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

        public bool Delete()
        {
            return clsApplicationsDataAccess.DeleteApplication(this.ApplicationID);


        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationsDataAccess.GetAllApplications();

        }

        public static bool IsApplicationExists(int ApplicationID)
        {
            return clsApplicationsDataAccess.IsApplicationExists(ApplicationID);

        }


        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationsDataAccess.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);

        }

        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationsDataAccess.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationsDataAccess.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }



    }
}
