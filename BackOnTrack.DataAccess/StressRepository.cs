using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace BackOnTrack.DataAccess
{
    public class StressRepository : IStressRepository
    {
        private readonly string? _connectionString;
        private SqlConnection sqlConnection;
        public StressRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            sqlConnection = new SqlConnection(_connectionString);
        }
        public bool CreateResult(StressResult result)
        {
            throw new NotImplementedException();
        }

        public bool DeleteResult(StressResult result)
        {
            throw new NotImplementedException();
        }

        public bool EditResult(StressResult result)
        {
            throw new NotImplementedException();
        }

        public List<StressResult> GetAllStresss(string userId)
        {
            List<StressResult> stressResults = new();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                string query = "SELECT s.Id, s.StressLevel, s.Date, s.User_Id, sl.HoursSlept as HoursSleptLastDay " +
                    "           FROM Stress AS s " +
                    "           INNER JOIN Sleepresults AS sl ON s.User_Id = sl.User_Id AND DATEADD(DAY, -1, s.Date) = sl.Date " +
                    "           WHERE s.User_Id = @userId ORDER BY Date Desc";
                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@userId", userId);

                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StressResult result = new StressResult
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                StressLevel = Convert.ToInt32(reader["StressLevel"]),
                                date = Convert.ToDateTime(reader["Date"]),
                                HoursSlept = Convert.ToInt32(reader["HoursSleptLastDay"]),
                                UserId = Convert.ToString(reader["User_Id"])
                            };
                            stressResults.Add(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle or log the exception as needed.
                    Console.WriteLine(ex.Message);
                }
            }

            return stressResults;
        }


        public StressResult GetStressByDate(string userId, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
