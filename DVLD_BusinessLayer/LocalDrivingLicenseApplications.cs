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
    public class clsLocalDrivingLicenseApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID {  get; set; }
        public int ApplicationID                    { get; set; }
        public int LicenseClassID                   { get; set; }

        public clsApplication Application { get; set; }
        public clsLicenseClass LicenseClass { get; set; }


        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            ApplicationID = -1;
            LicenseClassID = -1;
            this.Application = new clsApplication();
            this.LicenseClass = new clsLicenseClass();

            Mode = enMode.AddNew;

        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID , int ApplicationID, int LicenseClassID)
        { 
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;

            this.Application  = clsApplication.Find(ApplicationID);
            this.LicenseClass = clsLicenseClass.Find(LicenseClassID);

            Mode = enMode.Update;
        }


        public static clsLocalDrivingLicenseApplication Find(int Local_D_L_ApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            if(clsLocalDrivingLicenseApplicationDataAccess.GetLocalDrivingLicenseApplicationInfoByID(Local_D_L_ApplicationID,ref ApplicationID ,ref LicenseClassID))
                return new clsLocalDrivingLicenseApplication(Local_D_L_ApplicationID, ApplicationID, LicenseClassID);
            else 
                return null;
        }


        private bool _AddNewLocalDLApplication()
        {

            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationDataAccess.AddNewLocalDLApplication(this.ApplicationID,this.LicenseClassID);

            if (this.LocalDrivingLicenseApplicationID != -1)
            {
                this.Application = clsApplication.Find(ApplicationID);
                this.LicenseClass = clsLicenseClass.Find(LicenseClassID);
                return true;
            }

            return false;

        }

        private bool _UpdateLocalDLApplication()
        {

            return clsLocalDrivingLicenseApplicationDataAccess.UpdateLocalDLApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);


        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewLocalDLApplication())
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
                        if (_UpdateLocalDLApplication())
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


        public static DataTable GetAllLocalDLApplications()
        {
            return clsLocalDrivingLicenseApplicationDataAccess.GetAllLocalDLApplications();

        }

        public static DataTable GetAllLocalDLApplications_1()
        {
            DataTable dt = clsLocalDrivingLicenseApplicationDataAccess.GetAllLocalDLApplications_1();

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
           return clsLocalDrivingLicenseApplicationDataAccess.IsExistsApplicationByStatus(NationalNo, ClassName , Status);
        }

        public static int GetPassedTest(int LDLAppID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.GetPassedTest(LDLAppID);
        }

        public static string GetStatus(int LDLAppID)
        {

            return clsLocalDrivingLicenseApplicationDataAccess.GetStatus(LDLAppID);
        }


        public static bool DeleteLocalDLApplications(int LDLAppID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.DeleteLocalDLApplications(LDLAppID);

        }




    }


}
