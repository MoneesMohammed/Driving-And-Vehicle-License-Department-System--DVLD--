using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayar
{
    public class clsLicenseDataAccess
    {

        public static bool GetLicenseInfoByID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;



                    ApplicationID   = (int)reader["ApplicationID"];
                    DriverID        = (int)reader["DriverID"];
                    LicenseClass    = (int)reader["LicenseClass"];
                    IssueDate       = (DateTime)reader["IssueDate"];
                    ExpirationDate  = (DateTime)reader["ExpirationDate"];
                    PaidFees        = (decimal)reader["PaidFees"];
                    IsActive        = (bool)reader["IsActive"];
                    IssueReason     = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    Notes  = reader["Notes"] == DBNull.Value ? "": (string)reader["Notes"];

          

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

        public static bool GetLicenseInfoByApplicationID(ref int LicenseID,  int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;



                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    Notes = reader["Notes"] == DBNull.Value ? "" : (string)reader["Notes"];



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


        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO Licenses VALUES (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID);" +
                           "SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@ApplicationID"  , ApplicationID    );
            command.Parameters.AddWithValue("@DriverID"       , DriverID         );
            command.Parameters.AddWithValue("@LicenseClass"   , LicenseClass     );
            command.Parameters.AddWithValue("@IssueDate"      , IssueDate        );
            command.Parameters.AddWithValue("@ExpirationDate" ,ExpirationDate    );
            command.Parameters.AddWithValue("@PaidFees"       ,PaidFees          );
            command.Parameters.AddWithValue("@IsActive"       ,IsActive          );
            command.Parameters.AddWithValue("@IssueReason"    ,IssueReason       );
            command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID   );

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

        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int RowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Licenses SET ApplicationID=@ApplicationID ,DriverID=@DriverID ,LicenseClass=@LicenseClass ,IssueDate=@IssueDate ,ExpirationDate=@ExpirationDate,Notes=@Notes,PaidFees=@PaidFees,IsActive=@IsActive,IssueReason=@IssueReason,CreatedByUserID=@CreatedByUserID  " +
                           "WHERE LicenseID = @LicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
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


        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * From Licenses";

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


        public static int CountIssueReason(int DriverID, int LicenseClass)
        {
            int Count = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select Count(DriverID) as [Count] from Licenses  where DriverID = @DriverID AND LicenseClass = @LicenseClass;";
                      
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
           

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int CountIssueReason))
                {
                    Count = CountIssueReason;
                }

            }
            catch
            {
                Count = -1;

            }
            finally
            {

                connection.Close();
            }

            return Count;
        }

        public static DataTable GetAllLicenseByPersonID(int PersonID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT  Licenses.LicenseID, Applications.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive " +
                           "FROM    Applications INNER JOIN Licenses ON Applications.ApplicationID = Licenses.ApplicationID INNER JOIN LicenseClasses " +
                           "        ON Licenses.LicenseClass = LicenseClasses.LicenseClassID " +
                           "Where   Applications.ApplicantPersonID = @PersonID;";

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
