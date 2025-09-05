using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayar
{
    public class clsUsersDataAccess
    {
        public static bool GetUserInfoByID(int UserID, ref  int PersonID , ref string UserName , ref string Password , ref bool IsActive )
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

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


        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO Users VALUES (@PersonID , @UserName , @Password ,@IsActive);" +
                           "SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            

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

        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            int RowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Users SET PersonID=@PersonID ,UserName=@UserName ,Password=@Password ,IsActive=@IsActive " +
                           "WHERE UserID = @UserID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID"  , UserID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

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


        public static bool DeleteUser(int UserID)
        {
            int RowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

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



        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * From Users";

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


        public static DataTable GetAllUsers_1()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Users.UserID as [User ID], Users.PersonID as [Person ID],People.FirstName+' '+ People.SecondName +' '+ People.ThirdName+' '+ People.LastName as [Full Name], Users.UserName, Users.IsActive as [Is Active] " +
                           "FROM Users INNER JOIN People ON Users.PersonID = People.PersonID";

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

        public static bool IsUserExists(int UserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found = 1 FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;

                reader.Close();
            }
            catch //(Exception ex)
            {
                //Console.WriteLine("Error : " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();

            }

            return isFound;


        }


        public static bool GetUserInfoByUserName(ref int UserID, ref int PersonID, string UserName, ref string Password, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Users WHERE UserName = @UserName ;";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    UserID   = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];


                }

                reader.Close();


            }
            catch //(Exception ex)
            {
                //Console.WriteLine("Error : " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();

            }

            return isFound;


        }



        public static bool IsUserExistsByUserName(string UserName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found = 1 FROM Users WHERE UserName = @UserName ;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;

                reader.Close();
            }
            catch //(Exception ex)
            {
                //Console.WriteLine("Error : " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();

            }

            return isFound;

        }


        public static bool IsUserExistsByPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found = 1 FROM Users WHERE PersonID = @PersonID ;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;

                reader.Close();
            }
            catch //(Exception ex)
            {
                //Console.WriteLine("Error : " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();

            }

            return isFound;

        }



    }
}
