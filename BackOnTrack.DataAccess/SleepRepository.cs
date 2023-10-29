using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace BackOnTrack.DataAccess
{
    public class SleepRepository : ISleepRepository
    {
        private readonly string? _connectionString;
        private readonly IConfiguration _configuration;
        private SqlConnection sqlConnection;
        public SleepRepository()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            this._connectionString = _configuration.GetConnectionString("DefaultConnection");
            sqlConnection = new SqlConnection(_connectionString);
        }
        public bool CreateResult(SleepResult result)
        {
            using (sqlConnection = new SqlConnection(_connectionString)) // Create a new connection
            {
                string query = "INSERT INTO [Sleepresults] ([HoursSlept], [User_Id], [Date]) VALUES (@HoursSlept, @User_Id, @Date)";

                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@HoursSlept", result.HoursSlept);
                command.Parameters.AddWithValue("@User_Id", int.Parse(result.UserID));
                command.Parameters.AddWithValue("@Date", result.Date);

                try
                {
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    return true; // Success
                }
                catch (Exception ex)
                {
                    // Log the exception, don't re-throw it
                    Console.WriteLine(ex.Message);
                    return false; // Return false to indicate an error
                }
            }
        }




        public bool DeleteResult(SleepResult Delete)
        {
            using (sqlConnection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM [Sleepresults] WHERE [Id] = @SleepId AND [User_Id] = @UserId AND [HoursSlept] = @HoursSlept AND [Date] = @Date";

                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@SleepId", Delete.Id);
                command.Parameters.AddWithValue("@UserId", Delete.UserID);
                command.Parameters.AddWithValue("@HoursSlept", Delete.HoursSlept);
                command.Parameters.AddWithValue("@Date", Delete.Date);

                try
                {
                    sqlConnection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public List<SleepResult> GetAll()
        {
            List<SleepResult > sleepResults = new(); 

            using(sqlConnection = new SqlConnection(_connectionString))
            {
                string query = "SELECT [Id], [HoursSlept], [User_Id], [Date] FROM [SleepResults]";
                SqlCommand command = new(query, sqlConnection);

                try
                {
                    sqlConnection.Open();
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SleepResult result = new()
                            {
                                Id = Convert.ToInt32(reader["Id"]), // int.Parse() of int.TryParse()
                                HoursSlept = Convert.ToInt32(reader["HoursSlept"]),
                                Date = Convert.ToDateTime(reader["Date"]),
                                UserID = Convert.ToString(reader["user_id"])
                            };
                            sleepResults.Add(result);
                        }
                    }
                    sqlConnection.Close();
                }
                catch
                {

                }
                return sleepResults;
            }
        }

        public SleepResult GetById(int id)
        {
            SleepResult result = new();
            using (sqlConnection = new(_connectionString))
            {
                string query = "SELECT [Id], [HoursSlept], [User_Id], [Date] FROM [SleepResults] WHERE [Id] = @id";
                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                HoursSlept = Convert.ToInt32(reader["HoursSlept"]),
                                Date = Convert.ToDateTime(reader["Date"]),
                                UserID = Convert.ToString(reader["user_Id"])
                            };
                        }
                    }
                }
                catch
                {

                }
            }
            sqlConnection.Close();
            return result;
        }

        public List<SleepResult> GetLastSeven()
        {
            List<SleepResult> sleepResults = new();

            using (sqlConnection = new SqlConnection(_connectionString))
            {
                string query = "SELECT [Id], [HoursSlept], [User_Id], [Date] FROM [SleepResults] " +
                               "WHERE [Date] >= DATEADD(DAY, -7, GETDATE())";

                SqlCommand command = new(query, sqlConnection);

                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SleepResult result = new()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                HoursSlept = Convert.ToInt32(reader["HoursSlept"]),
                                Date = Convert.ToDateTime(reader["Date"]),
                                UserID = Convert.ToString(reader["user_Id"])
                            };
                            sleepResults.Add(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // Handle the exception as needed
                }
            }

            return sleepResults;
        }

        public SleepResult GetResultByDateAndUserId(DateTime inputDate, string userId)
        {
            SleepResult result = new();

            using (SqlConnection connection = new(_connectionString))
            {
                string query = "SELECT [Id], [HoursSlept], [User_Id], [Date] FROM [SleepResults] WHERE [User_Id] = @userid AND [Date] = @date";
                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@userid", userId);
                command.Parameters.AddWithValue("@date", inputDate);

                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new SleepResult
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                HoursSlept = Convert.ToInt32(reader["HoursSlept"]),
                                Date = Convert.ToDateTime(reader["Date"]),
                                UserID = Convert.ToString(reader["user_Id"])
                            };
                        }
                    }
                }
                catch
                {

                }
            }

            return result;
        }


        public bool UpdateResult(SleepResult result)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                string query = "UPDATE [Sleepresults] SET [HoursSlept] = @NewHoursSlept WHERE [User_Id] = @UserId AND [Id] = @SleepId";

                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@NewHoursSlept", result.HoursSlept);
                command.Parameters.AddWithValue("@UserId", result.UserID);
                command.Parameters.AddWithValue("@SleepId", result.Id);

                try
                {
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    return true; // Success
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false; // Return false to indicate an error
                }
            }
        }
    }
}
