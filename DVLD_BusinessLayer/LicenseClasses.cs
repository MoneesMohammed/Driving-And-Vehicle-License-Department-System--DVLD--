using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsLicenseClass
    {
        public int    LicenseClassID       { get; set; }
        public string ClassName             { get; set; }
        public string ClassDescription       { get; set; }
        public byte MinimumAllowedAge     { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees             { get; set; }

        public clsLicenseClass()
        {
            LicenseClassID         = -1;
            ClassName              = "";
            ClassDescription       = "";
            MinimumAllowedAge      = 0;
            DefaultValidityLength  = 0;
            ClassFees              = 0;
        }

        private clsLicenseClass(int LicenseClassID, string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, decimal ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
        }


        public static clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = "" , ClassDescription = "";
            byte MinimumAllowedAge = 0, DefaultValidityLength = 0;
            decimal ClassFees = 0;

            if (clsLicenseClassDataAccess.GetLicenseClassInfoByID(LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }


        public static clsLicenseClass Find(string ClassName)
        {
            int LicenseClassID = -1;
            string ClassDescription = "";
            byte MinimumAllowedAge = 0, DefaultValidityLength = 0;
            decimal ClassFees = 0;

            if (clsLicenseClassDataAccess.GetLicenseClassInfoByClassName(ref LicenseClassID,ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }


        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassDataAccess.GetAllLicenseClasses();

        }


    }
}
