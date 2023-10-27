using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using (sqlConnection)
            {
                string query = "INSERT INTO [Sleepresults] ([HoursSlept], [User_Id], [Date]) VALUES (@HoursSlept, @User_Id, @Date)";

                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@HoursSlept", result.HoursSlept);
                command.Parameters.AddWithValue("@User_Id", int.Parse(result.UserID));
                command.Parameters.AddWithValue("@Date", result.Date.ToString(""));

                try
                {
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
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
            using SqlConnection sqlConnection = new(_connectionString);

            string query = "DELETE FROM [Sleepresults] WHERE [Id] = @SleepId AND [User_Id] = @UserId AND [HoursSlept] = @HoursSlept AND [Date] = @Date";

            SqlCommand command = new SqlCommand(query, sqlConnection);
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

        public List<SleepResult> GetAll()
        {
            List<SleepResult > sleepResults = new List<SleepResult>(); 

            using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                string query = "SELECT [Id], [HoursSlept], [User_Id], [Date] FROM [SleepResults]";
                SqlCommand command = new SqlCommand(query, sqlConnection);

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
                }
                catch
                {

                }
                return sleepResults;
            }
        }

        public SleepResult GetById(int id)
        {
            SleepResult result = new SleepResult();
            using (sqlConnection)
            {
                string query = "SELECT [Id], [HoursSlept], [User_Id], [Date] FROM [SleepResults] WHERE [Id] = @id";
                SqlCommand command = new SqlCommand(query, sqlConnection);
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
            return result;
        }

        public List<SleepResult> GetLastSeven()
        {
            throw new NotImplementedException();
        }

        public bool UpdateResult(SleepResult result)
        {
            using (sqlConnection)
            {
                string query = "UPDATE [Sleepresults] SET [HoursSlept] = @NewHoursSlept WHERE [User_Id] = @UserId AND [Id] = @SleepId";

                SqlCommand command = new SqlCommand(query, sqlConnection);
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
