using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            IsActive = false;
            Mode = enMode.AddNew;
        }

        private clsUser(int UserID, int PersonID,  string UserName,  string Password, bool IsActive)
        { 
            this.UserID   = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }



        public static clsUser Find(int UserID)
        {
           int PersonID = -1;
           string UserName = ""   , Password = "";
           bool   IsActive = false;

            if (clsUsersDataAccess.GetUserInfoByID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive ))
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;

        }


        public static clsUser Find(string UserName)
        {
            int UserID = -1 , PersonID = -1;
            
            string Password = "";
            bool IsActive = false;

            if (clsUsersDataAccess.GetUserInfoByUserName(ref UserID, ref PersonID,  UserName, ref Password, ref IsActive))
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;


        }


        private bool _AddNewUser()
        {

            this.UserID = clsUsersDataAccess.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (UserID != -1);


        }

        private bool _UpdateUser()
        {

            return clsUsersDataAccess.UpdateUser(this.UserID ,this.PersonID, this.UserName, this.Password, this.IsActive);


        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewUser())
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
                        if (_UpdateUser())
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



        public static bool DeleteUser(int UserID)
        {
            return clsUsersDataAccess.DeleteUser(UserID);

        }


        public static DataTable GetAllUsers()
        {
            return clsUsersDataAccess.GetAllUsers();

        }

        public static DataTable GetAllUsers_1()
        {
            return clsUsersDataAccess.GetAllUsers_1();

        }

        public static bool IsUserExists(int UserID)
        {
            return clsUsersDataAccess.IsUserExists(UserID);
        }

        public static bool IsUserExistsByUserName(string UserName)
        {
            return clsUsersDataAccess.IsUserExistsByUserName(UserName);

        }

        public static bool IsUserExistsByPersonID(int PersonID)
        {
            return clsUsersDataAccess.IsUserExistsByPersonID(PersonID);

        }


    }




}

