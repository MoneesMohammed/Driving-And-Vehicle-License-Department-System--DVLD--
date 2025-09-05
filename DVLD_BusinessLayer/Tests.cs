using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

        public int    TestID                   { get; set; }
        public int    TestAppointmentID        { get; set; }
        public clsTestAppointment TestAppointment { get; set; }
        public bool   TestResult              { get; set; }
        public string Notes                 { get; set; }
        public int    CreatedByUserID          { get; set; }
        public clsUser CreatedByUser { get; set; }


        public clsTest()
        {

            TestID = -1;
            TestAppointmentID = -1;
            TestResult = true;
            Notes = "";
            CreatedByUserID = -1;

            TestAppointment = new clsTestAppointment();
            CreatedByUser = new clsUser();

            Mode = enMode.AddNew;

        }


        private clsTest(int TestID,int TestAppointmentID,bool TestResult,string Notes ,int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;

            TestAppointment = clsTestAppointment.Find(TestAppointmentID);
            CreatedByUser = clsUser.Find(CreatedByUserID);


            Mode = enMode.Update;

        }

        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1 , CreatedByUserID = -1;
            bool TestResult = true;
            string Notes = "";
            
            if(clsTestDataAccess.GetTestInfoByID(TestID ,ref TestAppointmentID,ref TestResult,ref Notes,ref CreatedByUserID))
                return new clsTest(TestID, TestAppointmentID, TestResult , Notes, CreatedByUserID);
            else 
                return null;

        }

        public static clsTest FindByTestAppointmentID(int TestAppointmentID)
        {
            int TestID = -1, CreatedByUserID = -1;
            bool TestResult = true;
            string Notes = "";

            if (clsTestDataAccess.GetTestInfoByTestAppointmentID(ref TestID, TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            else
                return null;

        }


        private bool _AddNewTest()
        {

            this.TestID = clsTestDataAccess.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);

            if (this.TestID != -1)
            {
                TestAppointment = clsTestAppointment.Find(TestAppointmentID);
                CreatedByUser = clsUser.Find(CreatedByUserID);
                return true;

            }

            return false;
        }

        private bool _UpdateTest()
        {

            return clsTestDataAccess.UpdateTest(this.TestID,this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewTest())
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
                        if (_UpdateTest())
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



        public static DataTable GetAllTest()
        {
            return clsTestDataAccess.GetAllTests();

        }


        public static bool CheckPassedTest(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            //he did not succeed if False


            return clsTestDataAccess.TestResult(TestTypeID, LocalDrivingLicenseApplicationID);
        }





    }


}
