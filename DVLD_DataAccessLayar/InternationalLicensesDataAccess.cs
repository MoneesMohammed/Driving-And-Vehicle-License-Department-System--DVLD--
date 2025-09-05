using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayar
{
    public class clsInternationalLicensesDataAccess
    {
        public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID,ref int ApplicationID,ref int DriverID,ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate,ref DateTime ExpirationDate,ref bool IsActive,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;


                    ApplicationID             = (int)reader["ApplicationID"];
                    DriverID                  = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate                 = (DateTime)reader["IssueDate"];
                    ExpirationDate            = (DateTime)reader["ExpirationDate"];
                    IsActive                  = (bool)reader["IsActive"];
                    CreatedByUserID           = (int)reader["CreatedByUserID"];

                    

                }


                reader.Close();
            }
            catch//(Exception e)
            {
                isFound = false;

            }
            finally
            {
                connection.Close();


            }

            return isFound;
        }


        public static bool GetInternationalLicenseInfoByLicenseID(ref int InternationalLicenseID, ref int ApplicationID, ref int DriverID,int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;


                    InternationalLicenseID = (int)reader["InternationalLicenseID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];



                }


                reader.Close();
            }
            catch//(Exception e)
            {
                isFound = false;

            }
            finally
            {
                connection.Close();


            }

            return isFound;
        }



        public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int LicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO InternationalLicenses VALUES (@ApplicationID , @DriverID , @LicenseID ,@IssueDate , @ExpirationDate , @IsActive ,@CreatedByUserID);" +
                           "SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@ApplicationID"  ,ApplicationID   );
            command.Parameters.AddWithValue("@DriverID"       ,DriverID        );
            command.Parameters.AddWithValue("@LicenseID"      ,LicenseID       );
            command.Parameters.AddWithValue("@IssueDate"      ,IssueDate       );
            command.Parameters.AddWithValue("@ExpirationDate" ,ExpirationDate  );
            command.Parameters.AddWithValue("@IsActive"       ,IsActive        );
            command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID );



            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                {
                    ID = insertedID;
                }

            }
            catch
            {
                ID = -1;

            }
            finally
            {

                connection.Close();
            }

            return ID;

        }

        public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int LicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int RowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE InternationalLicenses SET ApplicationID=@ApplicationID ,DriverID=@DriverID ,IssuedUsingLocalLicenseID=@LicenseID ,IssueDate=@IssueDate ,ExpirationDate=@ExpirationDate ,IsActive=@IsActive ,CreatedByUserID=@CreatedByUserID  " +
                           "WHERE InternationalLicenseID = @InternationalLicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                RowAffected = command.ExecuteNonQuery();


            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (RowAffected > 0);
        }



        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * From InternationalLicenses";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    dt.Load(reader);

                }

                reader.Close();
            }
            catch
            {



            }
            finally
            {
                connection.Close();


            }


            return dt;

        }


        public static DataTable GetAllInternationalLicenses_1()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT InternationalLicenseID as [Int.License ID] , ApplicationID as [Application ID],IssuedUsingLocalLicenseID as [L.License ID] ,DriverID as [Driver ID],IssueDate as[Issue Date] ,ExpirationDate as[Expiration Date] ,IsActive as[Is Active] " +
                           "FROM InternationalLicenses;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    dt.Load(reader);

                }

                reader.Close();
            }
            catch
            {



            }
            finally
            {
                connection.Close();


            }


            return dt;

        }


        public static DataTable GetInternationalLicensesByPersonID(int PersonID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT InternationalLicenses.InternationalLicenseID as [Int.License ID], Applications.ApplicationID as [Application ID], InternationalLicenses.IssuedUsingLocalLicenseID as [L.License ID], InternationalLicenses.IssueDate as[Issue Date], InternationalLicenses.ExpirationDate as[Expiration Date] , InternationalLicenses.IsActive as[Is Active] " +
                           "FROM  Applications INNER JOIN InternationalLicenses ON Applications.ApplicationID = InternationalLicenses.ApplicationID " +
                           "WHERE Applications.ApplicantPersonID = @PersonID; ";



            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    dt.Load(reader);

                }

                reader.Close();
            }
            catch
            {



            }
            finally
            {
                connection.Close();


            }


            return dt;

        }



    }
}
