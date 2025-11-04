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
        public clsTestType.enTestType TestTypeID      { get; set; }
        public int LocalDrivingLicenseApplicationID   { get; set; }
        public DateTime AppointmentDate               { get; set; }
        public decimal PaidFees                       { get; set; }
        public int CreatedByUserID                    { get; set; }
        public bool IsLocked                          { get; set; }
        public int RetakeTestApplicationID            { get; set; }
        public clsApplication RetakeTestAppInfo       { get; set; }

        public int TestID 
        { get { return _GetTestID(); }  }



        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = clsTestType.enTestType.VisionTest;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate  = DateTime.Now;
            PaidFees = -1;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;
              
            Mode = enMode.AddNew;
        }

        private clsTestAppointment(int TestAppointmentID, clsTestType.enTestType TestTypeID,int LocalDrivingLicenseApplicationID,DateTime AppointmentDate,decimal PaidFees,int CreatedByUserID,bool IsLocked, int RetakeTestApplicationID)
        { 
            this.TestAppointmentID                = TestAppointmentID;
            this.TestTypeID                       = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate                  = AppointmentDate;
            this.PaidFees                         = PaidFees;
            this.CreatedByUserID                  = CreatedByUserID;
            this.IsLocked                         = IsLocked;
            this.RetakeTestApplicationID          = RetakeTestApplicationID;

            RetakeTestAppInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID);

            Mode = enMode.Update;
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int      TestTypeID = -1 , LocalDrivingLicenseApplicationID = -1 ,CreatedByUserID = -1  , RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal  PaidFees = -1;
            bool     IsLocked = false;
            
            if (clsTestAppointmentDataAccess.GetTestAppointmentInfoByID(TestAppointmentID,ref TestTypeID,ref LocalDrivingLicenseApplicationID ,ref AppointmentDate ,ref PaidFees,ref CreatedByUserID ,ref IsLocked,ref RetakeTestApplicationID))
                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else 
                return null;

        }


        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1, RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = -1;
            bool IsLocked = false;

            if (clsTestAppointmentDataAccess.GetLastTestAppointment(LocalDrivingLicenseApplicationID,(int)TestTypeID, ref TestAppointmentID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }



        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentDataAccess.AddNewTestAppointment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);                                                                          
        }                                                                                                                              
                                                                                                                             
        private bool _UpdateTestAppointment()                                                                                         
        {

            return clsTestAppointmentDataAccess.UpdateTestAppointment(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked,this.RetakeTestApplicationID);
      
        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
        
                   if (_AddNewTestAppointment())
                   {
                       Mode = enMode.Update;
                       return true;
                   }
                   else
                   {
                       return false;
                   }
                  
                case enMode.Update:
                
                   return _UpdateTestAppointment();
                 
            }

            return false;
        }



        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = clsTestAppointmentDataAccess.GetAllTestAppointments();

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

        public DataTable GetApplicationTestAppointmentsPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentDataAccess.GetApplicationTestAppointmentsPerTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {

            return clsTestAppointmentDataAccess.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        private int _GetTestID()
        {
            return clsTestAppointmentDataAccess.GetTestID(this.TestAppointmentID);

        }

        //---------------------

        public static DataTable GetAllTestAppointments(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {

            return clsTestAppointmentDataAccess.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID,(int)TestTypeID);

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
