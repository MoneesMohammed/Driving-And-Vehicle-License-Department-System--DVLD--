using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayar
{
    public class clsPeopleDataAccess
    {
        public static bool GetPersonInfoByID(int PersonID, ref string NationalNo , ref string FirstName ,
            ref string SecondName , ref string ThirdName , ref string LastName , ref DateTime DateOfBirth ,
            ref byte Gendor , ref string Address , ref string Phone , ref string Email ,ref int NationalityCountryID ,
            ref string ImagePath)
            
        { 
            bool Isfound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Isfound = true;

                    NationalNo           = (string)reader["NationalNo"];
                    FirstName            = (string)reader["FirstName"];
                    SecondName           = (string)reader["SecondName"];
                    LastName             = (string)reader["LastName"];
                    DateOfBirth          = (DateTime)reader["DateOfBirth"];
                    Gendor               = (byte)reader["Gendor"];
                    Address              = (string)reader["Address"];
                    Phone                = (string)reader["Phone"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    ThirdName = reader["ThirdName"] == DBNull.Value ? "" : (string)reader["ThirdName"];
                    Email = reader["Email"] == DBNull.Value ? "" : (string)reader["Email"];
                    ImagePath            = reader["ImagePath"] == DBNull.Value ? "" : (string)reader["ImagePath"];



                }

                reader.Close();

            }
            catch
            {
                Isfound = false;
            }
            finally
            {
                connection.Close();
            }

            return Isfound;
        }


        public static int AddNewPerson(string NationalNo, string FirstName,
             string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
             byte Gendor, string Address, string Phone, string Email,int NationalityCountryID,
             string ImagePath)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO People VALUES (@NationalNo , @FirstName , @SecondName ,@ThirdName , @LastName ,@DateOfBirth , @Gendor ,@Address ,@Phone ,@Email ,@NationalityCountryID , @ImagePath);" +
                           "SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName" , FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if(ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


            if (Email != "")
               command.Parameters.AddWithValue("@Email", Email);
            else
               command.Parameters.AddWithValue("@Email", System.DBNull.Value);

           
            if(ImagePath != "")
              command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
              command.Parameters.AddWithValue("@ImagePath",System.DBNull.Value);

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


        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName,
             string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
             byte Gendor, string Address, string Phone, string Email, int NationalityCountryID,
             string ImagePath )
        {
            int RowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE People SET NationalNo=@NationalNo ,FirstName=@FirstName ,SecondName=@SecondName ,ThirdName=@ThirdName , LastName=@LastName , DateOfBirth=@DateOfBirth ,Gendor=@Gendor , Address=@Address ,Phone=@Phone , Email=@Email , NationalityCountryID=@NationalityCountryID , ImagePath=@ImagePath " +
                           "WHERE PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


            if (Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);


            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            try
            {
                connection.Open();

                RowAffected = command.ExecuteNonQuery();


            }
            catch//(Exception e)
            {
                
                return false;

            }
            finally
            {
                connection.Close();
            }

            return (RowAffected > 0);
        }



        public static bool DeletePerson(int PersonID)
        {
            int RowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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


        public static bool GetImagePathByID(int PersonID ,ref string ImagePath)
        {
            bool Isfound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT ImagePath FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && Result != DBNull.Value)
                {
                    Isfound = true;

                    ImagePath =  Result.ToString();

                }
                else
                {
                    Isfound = false;
                    ImagePath = "";

                }


            }
            catch
            {
                Isfound = false;
            }
            finally
            {
                connection.Close();
            }

            return Isfound;
        }


        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * From People";

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

        public static DataTable GetAllPeople_1()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT People.PersonID , People.NationalNo as [National No] , People.FirstName as [First Name], People.SecondName as [Second Name], People.ThirdName as [Third Name], People.LastName as [Last Name], " +
                "CASE" +
                "  WHEN People.Gendor = 0 THEN 'Male'" +
                "  WHEN People.Gendor = 1 THEN 'Female'" +
                "  END AS Gendor ," +
                "Countries.CountryName as Nationality ,People.DateOfBirth as [Date Of Birth], People.Phone, People.Email " +
                "FROM People INNER JOIN Countries ON People.NationalityCountryID = Countries.CountryID";

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


        public static bool IsPersonExists(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found = 1 FROM People WHERE PersonID = @PersonID";

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



        public static bool GetPersonInfoByNationalNo(ref int PersonID,  string NationalNo, ref string FirstName,
            ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
            ref byte Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID,
            ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo ;";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    PersonID   = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    ThirdName = reader["ThirdName"] == DBNull.Value ? "" : (string)reader["ThirdName"];

                    Email = reader["Email"] == DBNull.Value ? "" : (string)reader["Email"];

                    ImagePath = reader["ImagePath"] == DBNull.Value ? "" : (string)reader["ImagePath"];


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



        public static bool IsPersonExistsByNationalNo(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found = 1 FROM People WHERE NationalNo = @NationalNo ;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

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
