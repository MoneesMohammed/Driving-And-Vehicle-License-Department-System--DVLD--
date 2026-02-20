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
    public class clsTestTypeDataAccess
    {
        public static bool GetTestTypeInfoByID(int TestTypeID, ref string TestTypeTitle,ref string TestTypeDescription,ref decimal TestTypeFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];
                    TestTypeFees = (decimal)reader["TestTypeFees"];

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

        public static int AddNewTestType(string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO TestTypes VALUES (@TestTypeTitle , @TestTypeDescription , @TestTypeFees);" +
                           "SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);


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


        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            int RowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE TestTypes SET TestTypeTitle=@TestTypeTitle ,TestTypeDescription=@TestTypeDescription ,TestTypeFees=@TestTypeFees " +
                           "WHERE TestTypeID = @TestTypeID;";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

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


        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM TestTypes ORDER BY TestTypeID";

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

        public static DataTable GetAllTestTypes_1()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT TestTypeID as ID,TestTypeTitle as Title , TestTypeDescription as [Description] , TestTypeFees as Fees   FROM TestTypes ;";

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




    }
}
