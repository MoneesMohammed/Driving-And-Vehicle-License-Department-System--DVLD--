using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

       public    int DetainID                 { get; set; }
       public    int LicenseID                { get; set; }
       public    DateTime DetainDate          { get; set; }
       public    decimal FineFees             { get; set; }
       public    int CreatedByUserID          { get; set; }
       public    bool IsReleased              { get; set; }
       public    DateTime ReleaseDate         { get; set; }
       public    int ReleasedByUserID         { get; set; }
       public    int ReleaseApplicationID     { get; set; }

      

        public clsDetainedLicense()
        {
            this.DetainID               = -1;
            this.LicenseID              = -1;
            this.DetainDate             = DateTime.Now;
            this.FineFees               = 0;
            this.CreatedByUserID        = -1;
            this.IsReleased             = false;
            this.ReleaseDate            = DateTime.MinValue;
            this.ReleasedByUserID       = -1;
            this.ReleaseApplicationID   = -1;
            
            Mode = enMode.AddNew;
        }

        private clsDetainedLicense(int DetainID , int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {

            this.DetainID              = DetainID;
            this.LicenseID             = LicenseID;
            this.DetainDate            = DetainDate;
            this.FineFees              = FineFees;
            this.CreatedByUserID       = CreatedByUserID;
            this.IsReleased            = IsReleased;
            this.ReleaseDate           = ReleaseDate;
            this.ReleasedByUserID      = ReleasedByUserID;
            this.ReleaseApplicationID  = ReleaseApplicationID;

            Mode = enMode.Update;
        }

       


        public static clsDetainedLicense Find(int DetainID)
        {
            int LicenseID = -1, CreatedByUserID = -1, ReleaseApplicationID = -1, ReleasedByUserID = -1;
            DateTime DetainDate = DateTime.MinValue, ReleaseDate = DateTime.MinValue;
            decimal FineFees = 0;
            bool IsReleased = false;

           


            if (clsDetainedLicenseDataAccess.GetDetainedLicenseInfoByID(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
            {
                
                return null;
            }



        }



        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1, CreatedByUserID = -1, ReleaseApplicationID = -1, ReleasedByUserID = -1;
            DateTime DetainDate = DateTime.MinValue, ReleaseDate = DateTime.MinValue;
            decimal FineFees = 0;
            bool IsReleased = false;

            if (clsDetainedLicenseDataAccess.GetDetainedLicenseInfoByLicenseID(ref DetainID, LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;



        }





        private bool _AddNewDetainLicense()
        {
            
            this.DetainID = clsDetainedLicenseDataAccess.AddNewDetainLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);

            return (this.DetainID != -1);

        }


        private bool _UpdateDetainLicense()
        {

            return clsDetainedLicenseDataAccess.UpdateDetainLicense(this.DetainID,this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
            

        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewDetainLicense())
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
                        if (_UpdateDetainLicense())
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


        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseDataAccess.GetAllDetainedLicenses();

        }

        public static DataTable GetAllDetainedLicenses_1()
        {
            DataTable dt = clsDetainedLicenseDataAccess.GetAllDetainedLicenses_1();

            
            dt.Columns[0].ColumnName = "Detain ID";
            dt.Columns[1].ColumnName = "License ID";
            dt.Columns[2].ColumnName = "Detain Date";
            dt.Columns[3].ColumnName = "Is Released";
            dt.Columns[4].ColumnName = "Fine Fees";
            dt.Columns[5].ColumnName = "Release Date";
            dt.Columns[6].ColumnName = "National No.";
            dt.Columns[7].ColumnName = "Full Name";
            dt.Columns[8].ColumnName = "Release Application ID";


            return dt;
        }

        public static bool IsDetainLicenseExists(int DetainID)
        {

            return clsDetainedLicenseDataAccess.IsDetainLicenseExists(DetainID);

        }



        public static bool IsDetainLicenseExistsByLicenseID(int LicenseID)
        {

            return clsDetainedLicenseDataAccess.IsDetainLicenseExistsByLicenseID(LicenseID);
        
        }





    }
}
