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
        private enMode Mode = enMode.AddNew;

        public int                ApplicationID     {  get; set; }
        public int                ApplicantPersonID { get; set; }
        public clsPerson          Person            { get; set; }
        public DateTime           ApplicationDate   { get; set; }
        public int                ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationType   { get; set; }
        public byte               ApplicationStatus { get; set; }
        public DateTime           LastStatusDate    { get; set; }
        public decimal            PaidFees          { get; set; }
        public int                CreatedByUserID   { get; set; }
        public clsUser            CreatedByUser     { get; set; }


        public clsApplication()
        {
            ApplicationID = -1;
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.MinValue;
            ApplicationTypeID = -1;
            ApplicationStatus = 0;
            LastStatusDate = DateTime.MinValue;
            PaidFees = 0;
            CreatedByUserID = -1;

            Person = new clsPerson();
            ApplicationType = new clsApplicationType();
            CreatedByUser = new clsUser();

            Mode = enMode.AddNew;

        }

        private clsApplication(int ApplicationID,int ApplicantPersonID,DateTime ApplicationDate,int ApplicationTypeID,byte ApplicationStatus,DateTime LastStatusDate,decimal PaidFees,int CreatedByUserID)
        {
            this.ApplicationID     = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate   = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate    = LastStatusDate;
            this.PaidFees          = PaidFees;
            this.CreatedByUserID   = CreatedByUserID;

            Person = clsPerson.Find(ApplicantPersonID);
            ApplicationType = clsApplicationType.Find(ApplicationTypeID);
            CreatedByUser = clsUser.Find(CreatedByUserID);


            Mode = enMode.Update;
        }

        public static clsApplication Find(int ApplicationID)
        {
            int ApplicantPersonID = -1, ApplicationTypeID = -1, CreatedByUserID = -1;
            DateTime ApplicationDate = DateTime.MinValue, LastStatusDate = DateTime.MinValue;
            decimal PaidFees = 0;
            byte ApplicationStatus = 0;

            if (clsApplicationsDataAccess.GetApplicationInfoByID(ApplicationID,ref ApplicantPersonID,ref ApplicationDate,ref ApplicationTypeID,ref ApplicationStatus,ref LastStatusDate,ref PaidFees,ref CreatedByUserID))
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            else
                return null;
        }

        


        private bool _AddNewApplication()
        {

            this.ApplicationID = clsApplicationsDataAccess.AddNewApplication(this.ApplicantPersonID,this.ApplicationDate ,this.ApplicationTypeID,this.ApplicationStatus,this.LastStatusDate,this.PaidFees,this.CreatedByUserID);

            if (this.ApplicationID != -1)
            {
                Person = clsPerson.Find(ApplicantPersonID);
                ApplicationType = clsApplicationType.Find(ApplicationTypeID);
                CreatedByUser = clsUser.Find(CreatedByUserID);

                return true;    
            }

            return false;
        }

        private bool _UpdateApplication()
        {

            return clsApplicationsDataAccess.UpdateApplication(this.ApplicationID,this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);


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


        public static DataTable GetAllApplications()
        {
            return clsApplicationsDataAccess.GetAllApplications();

        }


        


    }
}
