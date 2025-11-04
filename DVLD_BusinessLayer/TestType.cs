using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayar
{
    public class clsTestType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public enTestType TestTypeID { get; set; }
       // public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        public clsTestType()
        {
            TestTypeID = clsTestType.enTestType.VisionTest;

            TestTypeTitle = string.Empty;
            TestTypeDescription = string.Empty;
            TestTypeFees = 0;

            Mode = enMode.AddNew;
        }

        private clsTestType(enTestType TestTypeID, string TestTypeTitle , string TestTypeDescription , decimal TestTypeFees)
        { 
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;

            Mode = enMode.Update;
        }


        public static clsTestType Find(enTestType TestTypeID)
        {
           
            string TestTypeTitle = "" , TestTypeDescription ="";
            decimal TestTypeFees = 0;

            if (clsTestTypeDataAccess.GetTestTypeInfoByID((int)TestTypeID,ref TestTypeTitle , ref TestTypeDescription , ref TestTypeFees))
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);

            else
              return null;

        }

        private bool _AddNewTestType()
        {
            //call DataAccess Layer 

            this.TestTypeID = (clsTestType.enTestType)clsTestTypeDataAccess.AddNewTestType(this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);

            return (this.TestTypeTitle != "");
        }


        private bool _UpdateTestType()
        {

            return clsTestTypeDataAccess.UpdateTestType((int)this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);


        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestType();

            }

            return false;
        }


        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeDataAccess.GetAllTestTypes();

        }

        public static DataTable GetAllTestTypes_1()
        {
            return clsTestTypeDataAccess.GetAllTestTypes_1();

        }





    }
}
