using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

        public int TestAppointmentID                  { get; set; }
        public int TestTypeID                         { get; set; }
        public clsTestType TestType { get; set; }
        public int LocalDrivingLicenseApplicationID   { get; set; }
        public clsLocalDrivingLicenseApplication LDLApplication { get; set; }
        public DateTime AppointmentDate               { get; set; }
        public decimal PaidFees                       { get; set; }
        public int CreatedByUserID                    { get; set; }
        public clsUser User                           { get; set; }
        public bool IsLocked                          { get; set; }
        public int RetakeTestApplicationID            { get; set; }

        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate  = DateTime.MinValue;
            PaidFees = -1;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;
            TestType = new clsTestType();
            LDLApplication = new clsLocalDrivingLicenseApplication();
            User = new clsUser();   

            Mode = enMode.AddNew;
        }

        private clsTestAppointment(int TestAppointmentID,int TestTypeID,int LocalDrivingLicenseApplicationID,DateTime AppointmentDate,decimal PaidFees,int CreatedByUserID,bool IsLocked, int RetakeTestApplicationID)
        { 
            this.TestAppointmentID                = TestAppointmentID;
            this.TestTypeID                       = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate                  = AppointmentDate;
            this.PaidFees                         = PaidFees;
            this.CreatedByUserID                  = CreatedByUserID;
            this.IsLocked                         = IsLocked;
            this.RetakeTestApplicationID          = RetakeTestApplicationID;

            TestType = clsTestType.Find(TestTypeID);
            LDLApplication = clsLocalDrivingLicenseApplication.Find(LocalDrivingLicenseApplicationID);
            User = clsUser.Find(CreatedByUserID);

            Mode = enMode.Update;
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int      TestTypeID = -1 , LocalDrivingLicenseApplicationID = -1 ,CreatedByUserID = -1  , RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.MinValue;
            decimal  PaidFees = -1;
            bool     IsLocked = false;
            
            if (clsTestAppointmentDataAccess.GetTestAppointmentInfoByID(TestAppointmentID,ref TestTypeID,ref LocalDrivingLicenseApplicationID ,ref AppointmentDate ,ref PaidFees,ref CreatedByUserID ,ref IsLocked,ref RetakeTestApplicationID))
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else 
                return null;

        }


        public static clsTestAppointment Find(int TestTypeID,int LocalDrivingLicenseApplicationID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1, RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.MinValue;
            decimal PaidFees = -1;
            bool IsLocked = false;

            if (clsTestAppointmentDataAccess.GetTAppointmentInfoByTestTypeIDAndLDLAppID(ref TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked,ref RetakeTestApplicationID))
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }



        private bool _AddNewTestAppointment()
        {

            this.TestAppointmentID = clsTestAppointmentDataAccess.AddNewTestAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);

            if (this.TestAppointmentID != -1)
            {
                TestType = clsTestType.Find(this.TestTypeID);
                LDLApplication = clsLocalDrivingLicenseApplication.Find(this.LocalDrivingLicenseApplicationID);
                User = clsUser.Find(this.CreatedByUserID);

                return true;
            }

            return false;                                                                          
        }                                                                                                                              
                                                                                                                             
        private bool _UpdateTestAppointment()                                                                                         
        {

            return clsTestAppointmentDataAccess.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked,this.RetakeTestApplicationID);
      
        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                {
                    if (_AddNewTestAppointment())
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
                   if (_UpdateTestAppointment())
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



        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentDataAccess.GetAllTestAppointments();

        }

        public static DataTable GetAllTestAppointments_1()
        {
            DataTable dt = clsTestAppointmentDataAccess.GetAllTestAppointments_1();

            //dt.Columns[0].ColumnName = "L.D.L.AppID";
            //dt.Columns[1].ColumnName = "Driving Class";
            //dt.Columns[2].ColumnName = "National No.";
            //dt.Columns[3].ColumnName = "Full Name";
            //dt.Columns[4].ColumnName = "Application Date";
            //dt.Columns[5].ColumnName = "Passed Tests";
            //dt.Columns[6].ColumnName = "Status";
            //dt.Columns[7].ColumnName = "Status";


            return dt;

        }

        public static DataTable GetAllTestAppointments(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {

            return clsTestAppointmentDataAccess.GetAllTestAppointmentsByTestTypeIDandLDLAppID(TestTypeID, LocalDrivingLicenseApplicationID);


        }

        public static bool AllIsLockedByTestTypeIDAndLDLAppID(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            return clsTestAppointmentDataAccess.AllIsLocked(TestTypeID, LocalDrivingLicenseApplicationID);

        }

        public static bool IsTestAppointmentExists(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            return clsTestAppointmentDataAccess.IsTestAppointmentExists(TestTypeID, LocalDrivingLicenseApplicationID);


        }

        public static int CountRetakeTest(int TestTypeID, int LocalDrivingLicenseApplicationID)
        { 
            return clsTestAppointmentDataAccess.GetCountRetakeTest(TestTypeID, LocalDrivingLicenseApplicationID);
        }


    }


}
