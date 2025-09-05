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
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        public clsTestType()
        {
            TestTypeID = -1;

            TestTypeTitle = string.Empty;
            TestTypeDescription = string.Empty;
            TestTypeFees = 0;


        }

        private clsTestType(int TestTypeID, string TestTypeTitle , string TestTypeDescription , decimal TestTypeFees)
        { 
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        
        }


        public static clsTestType Find(int TestTypeID)
        {
           
            string TestTypeTitle = "" , TestTypeDescription ="";
            decimal TestTypeFees = 0;

            if (clsTestTypeDataAccess.GetTestTypeInfoByID(TestTypeID,ref TestTypeTitle , ref TestTypeDescription , ref TestTypeFees))
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);

            else
              return null;

        }



        private bool _UpdateTestType()
        {

            return clsTestTypeDataAccess.UpdateTestType(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);


        }

        public bool Save()
        {
            if (_UpdateTestType())
            {

                return true;

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
