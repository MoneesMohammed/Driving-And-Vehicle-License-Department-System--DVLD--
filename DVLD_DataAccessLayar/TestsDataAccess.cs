using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayar
{
    public class clsTestDataAccess
    {
        public static bool GetTestInfoByID(int TestID,ref int TestAppointmentID,ref bool TestResult,ref string Notes,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Tests WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;


                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult        = (bool)reader["TestResult"];
                    Notes             = reader["Notes"] == DBNull.Value ? "" : (string)reader["Notes"];
                    CreatedByUserID   = (int)reader["CreatedByUserID"];


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

        public static bool GetTestInfoByTestAppointmentID(ref int TestID, int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Tests WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;


                    TestID = (int)reader["TestID"];
                    TestResult = (bool)reader["TestResult"];
                    Notes = reader["Notes"] == DBNull.Value ? "" : (string)reader["Notes"];
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

        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO Tests VALUES (@TestAppointmentID, @TestResult , @Notes, @CreatedByUserID);" +
                           "SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            

            if (Notes != "")
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);


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

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int RowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Tests SET TestAppointmentID=@TestAppointmentID ,TestResult=@TestResult ,Notes=@Notes ,CreatedByUserID=@CreatedByUserID  " +
                           "WHERE TestID = @TestID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (Notes != "")
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);


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


        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * From Tests";

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

        public static bool TestResult(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            bool Pass = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select TestResult from Tests where TestAppointmentID IN (SELECT TestAppointmentID FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestTypeID = @TestTypeID And IsLocked = 1) And TestResult = 1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                   
                   Pass = (bool)reader["TestResult"];
                       
                }

                reader.Close();
            }
            catch
            {

                Pass = false;

            }
            finally
            {
                connection.Close();


            }


            return Pass;

        }


    }

}
