﻿using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace BackOnTrack.DataAccess
{
    public class StressRepository : IStressRepository
    {
        private readonly string? _connectionString;
        public StressRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SchoolConnection");
        }
        public bool CreateResult(StressResult result)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                SqlTransaction transaction = sqlConnection.BeginTransaction();

                try
                {
                    string insertQuery = @"INSERT INTO Stress (StressLevel, Date, User_Id) 
                                           VALUES (@StressLevel, @Date, @UserId);
                                           SELECT SCOPE_IDENTITY();";

                    SqlCommand command = new SqlCommand(insertQuery, sqlConnection, transaction);
                    command.Parameters.AddWithValue("@StressLevel", result.StressLevel);
                    command.Parameters.AddWithValue("@Date", result.date);
                    command.Parameters.AddWithValue("@UserId", result.UserId);

                    // Execute the query to insert into Stress and retrieve the generated ID
                    int stressId = Convert.ToInt32(command.ExecuteScalar());

                    // If successful, commit the transaction
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    // Log the exception, and roll back the transaction on failure
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    return false;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }


        public bool DeleteResult(StressResult result)
        {
            using (SqlConnection sqlConnection = new(_connectionString))
            {
                string query = "DELETE FROM Stress WHERE Id = @Id";
                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@Id", result.Id);

                try
                {
                    sqlConnection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Handle or log the exception as needed.
                    Console.WriteLine(ex.Message);
                    return false; // Return false to indicate failure.
                }
            }
        }


        public bool EditResult(StressResult result)
        {
            using (SqlConnection sqlConnection = new(_connectionString))
            {
                string query = "UPDATE Stress SET StressLevel = @StressLevel, Date = @Date, User_Id = @UserId WHERE Id = @Id";
                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@StressLevel", result.StressLevel);
                command.Parameters.AddWithValue("@Date", result.date);
                command.Parameters.AddWithValue("@UserId", result.UserId);
                command.Parameters.AddWithValue("@Id", result.Id);

                try
                {
                    sqlConnection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Handle or log the exception as needed.
                    Console.WriteLine(ex.Message);
                    return false; // Return false to indicate failure.
                }
            }
        }


        public List<StressResult> GetAllStresss(string userId)
        {
            List<StressResult> stressResults = new();

            using (SqlConnection sqlConnection = new(_connectionString))
            {
                string query = """
                SELECT s.Id, s.StressLevel, s.Date, s.User_Id, ISNULL(sl.HoursSlept, 0) as HoursSleptLastDay
                FROM Stress AS s
                LEFT JOIN Sleepresults AS sl 
                  ON s.User_Id = sl.User_Id 
                  AND DATEADD(DAY, -1, s.Date) = sl.Date
                WHERE s.User_Id = @userId
                ORDER BY Date Desc
                """;
                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@userId", userId);

                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StressResult result = new()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                StressLevel = Convert.ToInt32(reader["StressLevel"]),
                                date = Convert.ToDateTime(reader["Date"]),
                                HoursSlept = Convert.ToInt32(reader["HoursSleptLastDay"]),
                                UserId = 
                                Convert.ToString(reader["User_Id"])
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

        public StressResult GetResultById(int id)
        {
            StressResult stressResult = new();

            using (SqlConnection sqlConnection = new(_connectionString))
            {
                string query = "SELECT s.Id, s.StressLevel, s.Date, s.User_Id, sl.HoursSlept as HoursSleptLastDay " +
                    "FROM Stress AS s " +
                    "INNER JOIN Sleepresults AS sl ON s.User_Id = sl.User_Id AND DATEADD(DAY, -1, s.Date) = sl.Date " +
                    "WHERE s.Id = @id";
                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stressResult = new StressResult
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                StressLevel = Convert.ToInt32(reader["StressLevel"]),
                                date = Convert.ToDateTime(reader["Date"]),
                                HoursSlept = Convert.ToInt32(reader["HoursSleptLastDay"]),
                                UserId = reader["User_Id"]?.ToString() ?? ""
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle or log the exception as needed.
                    Console.WriteLine(ex.Message);
                }
            }

            return stressResult;
        }


        public StressResult GetStressByDate(string userId, DateTime date)
        {
            StressResult stressResult = new();

            using (SqlConnection sqlConnection = new(_connectionString))
            {
                string query = "SELECT s.Id, s.StressLevel, s.Date, s.User_Id, sl.HoursSlept as HoursSleptLastDay " +
                    "FROM Stress AS s " +
                    "INNER JOIN Sleepresults AS sl ON s.User_Id = sl.User_Id AND DATEADD(DAY, -1, s.Date) = sl.Date " +
                    "WHERE s.User_Id = @userId AND s.Date = @date";
                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@date", date);

                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stressResult = new StressResult
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                StressLevel = Convert.ToInt32(reader["StressLevel"]),
                                date = Convert.ToDateTime(reader["Date"]),
                                HoursSlept = Convert.ToInt32(reader["HoursSleptLastDay"]),
                                UserId = Convert.ToString(reader["User_Id"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle or log the exception as needed.
                    Console.WriteLine(ex.Message);
                }
            }

            return stressResult;
        }

        public StressResult GetStressByDateAndId(DateTime date, string userId)
        {
            StressResult stressResult = new();

            using (SqlConnection sqlConnection = new(_connectionString))
            {
                string query = "SELECT s.Id, s.StressLevel, s.Date, s.User_Id, sl.HoursSlept as HoursSleptLastDay " +
                    "FROM Stress AS s " +
                    "INNER JOIN Sleepresults AS sl ON s.User_Id = sl.User_Id AND DATEADD(DAY, -1, s.Date) = sl.Date " +
                    "WHERE s.User_Id = @userid AND s.Date = @date"; // Change @id to @Id
                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@userid", userId); // Change parameter name from @id to @Id
                command.Parameters.AddWithValue("@date", date);

                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stressResult = new StressResult
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                StressLevel = Convert.ToInt32(reader["StressLevel"]),
                                date = Convert.ToDateTime(reader["Date"]),
                                HoursSlept = Convert.ToInt32(reader["HoursSleptLastDay"]),
                                UserId = Convert.ToString(reader["User_Id"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle or log the exception as needed.
                    Console.WriteLine(ex.Message);
                }
            }

            return stressResult;
        }

    }
}
