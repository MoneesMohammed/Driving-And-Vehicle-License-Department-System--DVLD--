using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayar
{
    public class clsTestDataAccess
    {
        public static bool GetTestInfoByID(int TestID ,ref int TestAppointmentID,ref bool TestResult,ref string Notes,ref int CreatedByUserID)
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
            catch (Exception ex)
            {
                clsUtil.WriteOnEventLog("DVLD", ex.Message, EventLogEntryType.Error);
                isFound = false;

            }
            finally
            {
                connection.Close();


            }

            return isFound;

        }

        public static bool GetLastTestInfoByPersonIDAndTestTypeAndLicenseClass(int PersonID ,int LicenseClassID ,int TestTypeID, ref int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT top 1  Tests.* " +
                           "FROM    Tests INNER JOIN TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID INNER JOIN " +
                           "LocalDrivingLicenseApplications ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID " +
                           "Where TestAppointments.TestTypeID = @TestTypeID AND LocalDrivingLicenseApplications.LicenseClassID =@LicenseClassID AND " +
                           "LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = " +
                           "(SELECT  LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID  " +
                           "FROM LocalDrivingLicenseApplications INNER JOIN  Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID " +
                           "Where Applications.ApplicantPersonID = @PersonID) " +
                           "Order By Tests.TestAppointmentID DESC; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID );
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID );
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID );

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    Notes = reader["Notes"] == DBNull.Value ? "" : (string)reader["Notes"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];


                }


                reader.Close();
            }
            catch (Exception ex)
            {
                clsUtil.WriteOnEventLog("DVLD", ex.Message, EventLogEntryType.Error);
                isFound = false;

            }
            finally
            {
                connection.Close();


            }

            return isFound;

        }

        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * From Tests Order By TestID";

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
            catch (Exception ex)
            {
                clsUtil.WriteOnEventLog("DVLD", ex.Message, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();


            }


            return dt;

        }

        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID) " +
                           "VALUES (@TestAppointmentID, @TestResult , @Notes, @CreatedByUserID); " +
                           "UPDATE TestAppointments SET IsLocked = 1 WHERE  TestAppointmentID = @TestAppointmentID ; " +
                           "SELECT SCOPE_IDENTITY(); ";

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
            catch (Exception ex)
            {
                clsUtil.WriteOnEventLog("DVLD", ex.Message, EventLogEntryType.Error);
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
            catch (Exception ex)
            {
                clsUtil.WriteOnEventLog("DVLD", ex.Message, EventLogEntryType.Error);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (RowAffected > 0);

        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT PassedTestCount = count(TestTypeID) " +
                           "FROM   Tests INNER JOIN  TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID " +
                           "Where  TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND Tests.TestResult = 1; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();

                if (Result != null && byte.TryParse(Result.ToString(), out byte PTCount))
                {
                    PassedTestCount = PTCount;
                }

            }
            catch (Exception ex)
            {
                clsUtil.WriteOnEventLog("DVLD", ex.Message, EventLogEntryType.Error);
                PassedTestCount = 0;

            }
            finally
            {

                connection.Close();
            }

            return PassedTestCount;

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
            catch (Exception ex)
            {
                clsUtil.WriteOnEventLog("DVLD", ex.Message, EventLogEntryType.Error);
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
