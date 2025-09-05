using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayar
{
    public class clsLocalDrivingLicenseApplicationDataAccess
    {
        public static bool GetLocalDrivingLicenseApplicationInfoByID(int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;


                    ApplicationID  = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                    
                    

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


        public static int AddNewLocalDLApplication(int ApplicationID, int LicenseClassID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO LocalDrivingLicenseApplications VALUES (@ApplicationID , @LicenseClassID );" +
                           "SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            


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

        public static bool UpdateLocalDLApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int RowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE LocalDrivingLicenseApplications SET ApplicationID=@ApplicationID ,LicenseClassID=@LicenseClassID  " +
                           "WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            

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


        public static DataTable GetAllLocalDLApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * From LocalDrivingLicenseApplications";

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


        public static DataTable GetAllLocalDLApplications_1()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM LocalDrivingLicenseApplications_View;";

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


        public static bool IsExistsApplicationByStatus(string NationalNo, string ClassName , string Status )
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found = 1 FROM LocalDrivingLicenseApplications_View where NationalNo = @NationalNo AND ClassName = @ClassName And Status = @Status;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@ClassName" , ClassName);
            command.Parameters.AddWithValue("@Status", Status);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
               

                reader.Close();
            }
            catch
            {

                isFound = false;

            }
            finally
            {
                connection.Close();


            }

            return isFound;
        }

        public static int GetPassedTest(int LDLAppID)
        {
            int PassedTest = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT PassedTestCount FROM LocalDrivingLicenseApplications_View where LocalDrivingLicenseApplicationID = @LDLAppID ;";
                          
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int PassTest))
                {
                    PassedTest = PassTest;
                }

            }
            catch
            {
                PassedTest = -1;

            }
            finally
            {

                connection.Close();
            }

            return PassedTest;



        }


        public static string GetStatus(int LDLAppID)
        {
            string Status = "";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Status FROM LocalDrivingLicenseApplications_View where LocalDrivingLicenseApplicationID = @LDLAppID ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);


            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();

                if (Result != null )
                {
                    Status = Result.ToString();
                }

            }
            catch
            {
                Status = "";

            }
            finally
            {

                connection.Close();
            }

            return Status;



        }


        public static bool DeleteLocalDLApplications(int LDLAppID)
        {
            int RowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DECLARE @AppID INT; " +

                          "SELECT @AppID = ApplicationID " +
                          "FROM LocalDrivingLicenseApplications " +
                          "WHERE LocalDrivingLicenseApplicationID = @LDLAppID; " +

                          "DELETE FROM LocalDrivingLicenseApplications " +
                          "WHERE LocalDrivingLicenseApplicationID = @LDLAppID;" +

                          "DELETE FROM Applications " +
                          "WHERE ApplicationID = @AppID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);

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


    }
}
