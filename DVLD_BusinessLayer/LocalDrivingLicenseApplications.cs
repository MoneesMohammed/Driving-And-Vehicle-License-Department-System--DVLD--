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
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID {  get; set; }
        public int LicenseClassID                   { get; set; }
        public clsLicenseClass LicenseClassInfo;

        public string PersonFullName
        {
            get 
            {
                return base.ApplicantFullName;
            } 
        
        
        }


        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = -1;
            
            Mode = enMode.AddNew;

        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID , int ApplicationID,int ApplicantPersonID,DateTime ApplicationDate,
               enApplicationType ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID , int LicenseClassID )
        { 
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            base.ApplicationID                    = ApplicationID;
            base.ApplicantPersonID                = ApplicantPersonID;
            base.ApplicationDate                  = ApplicationDate;
            base.ApplicationTypeID                = (int)ApplicationTypeID;
            base.ApplicationStatus                = ApplicationStatus;
            base.LastStatusDate                   = LastStatusDate;
            base.PaidFees                         = PaidFees;
            base.CreatedByUserID                  = CreatedByUserID;


            this.LicenseClassID = LicenseClassID;
            LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);

            ApplicationTypeInfo = clsApplicationType.Find((int)ApplicationTypeID);
            CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);


            Mode = enMode.Update;
        }


        public static clsLocalDrivingLicenseApplication FindByLocalDrivingApplicationID(int Local_D_L_ApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID(Local_D_L_ApplicationID, ref ApplicationID, ref LicenseClassID);

            if (IsFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(Local_D_L_ApplicationID, ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate, (enApplicationType)Application.ApplicationTypeID, (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
            {
                return null;
            }
               
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int Local_D_L_ApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID, ref Local_D_L_ApplicationID, ref LicenseClassID);

            if (IsFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLicenseApplication(Local_D_L_ApplicationID, ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate, (enApplicationType)Application.ApplicationTypeID, (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            } 
            else
                return null;
        }


        private bool _AddNewLocalDLApplication()
        {
          this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDLApplication(this.ApplicationID,this.LicenseClassID);

          return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDLApplication()
        {

            return clsLocalDrivingLicenseApplicationData.UpdateLocalDLApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);


        }



        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if(!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                  
                     if (_AddNewLocalDLApplication())
                     {
                         Mode = enMode.Update;
                         return true;
                     
                     }
                     else
                     {
                         return false;
                     }

                case enMode.Update:
                
                    return _UpdateLocalDLApplication();
                       
            }

            return false;
        }

        public bool Delete()
        {
            if (!base.Delete())
                return false;

            return clsLocalDrivingLicenseApplicationData.DeleteLocalDLApplications(this.LocalDrivingLicenseApplicationID);
        }

        public static DataTable GetAllLocalDLApplications()
        {
            DataTable dt = clsLocalDrivingLicenseApplicationData.GetAllLocalDLApplications();

            dt.Columns[0].ColumnName = "L.D.L.AppID";
            dt.Columns[1].ColumnName = "Driving Class";
            dt.Columns[2].ColumnName = "National No.";
            dt.Columns[3].ColumnName = "Full Name";
            dt.Columns[4].ColumnName = "Application Date";
            dt.Columns[5].ColumnName = "Passed Tests";
            dt.Columns[6].ColumnName = "Status";


            return dt;

        }


        public static bool IsExistsApplicationByStatus(string NationalNo, string ClassName, string Status = "New")
        {
           return clsLocalDrivingLicenseApplicationData.IsExistsApplicationByStatus(NationalNo, ClassName , Status);
        }

        public int GetPassedTestCount()
        {
            return clsLocalDrivingLicenseApplicationData.GetPassedTest(LocalDrivingLicenseApplicationID);
        }

        public static string GetStatus(int LDLAppID)
        {

            return clsLocalDrivingLicenseApplicationData.GetStatus(LDLAppID);
        }


        public static bool DeleteLocalDLApplications(int LDLAppID)
        {
            return clsLocalDrivingLicenseApplicationData.DeleteLocalDLApplications(LDLAppID);

        }



        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesPassPreviousTest(clsTestType.enTestType CurrentTestType)
        {

            switch (CurrentTestType)
            {
                case clsTestType.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.

                    return this.DoesPassTestType(clsTestType.enTestType.VisionTest);


                case clsTestType.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    return this.DoesPassTestType(clsTestType.enTestType.WrittenTest);

                default:
                    return false;
            }
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public bool AttendedTest(clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {

            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)

        {

            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        //public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        //{
        //    return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        //}

        //public byte GetPassedTestCount()
        //{
        //    return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        //}

        //public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        //{
        //    return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        //}

        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        //public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        //{
        //    //if total passed test less than 3 it will return false otherwise will return true
        //    return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        //}

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDriver();

                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            //now we diver is there, so we add new licesnse

            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClass = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.SetComplete();

                return License.LicenseID;
            }

            else
                return -1;
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }



    }


}
