using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace BackOnTrack.DataAccess
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly string? _connectionString;
        private SqlConnection sqlConnection;
        public ToDoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            sqlConnection = new SqlConnection(_connectionString);
        }
        public bool CreateToDo(ToDo toDo)
        {
            using (sqlConnection = new SqlConnection(_connectionString)) // Create a new connection
            {
                string query = "INSERT INTO [ToDo] ([Name], [Description], [Date], [User_Id], [Status]) VALUES (@name, @description, @date, @userId, @status)";

                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@name", toDo.Name);
                command.Parameters.AddWithValue("@description", toDo.Description);
                command.Parameters.AddWithValue("@date", toDo.PlannedDate);
                command.Parameters.AddWithValue("@userId", toDo.UserId);
                command.Parameters.AddWithValue("@status", toDo.Status);

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
            };
        }

        public bool DeleteToDo(ToDo toDo)
        {
            using (sqlConnection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM [ToDo] WHERE [Id] = @SleepId AND [User_Id] = @UserId AND [Name] = @Name AND [Date] = @Date";

                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@SleepId", toDo.Id);
                command.Parameters.AddWithValue("@UserId", toDo.UserId);
                command.Parameters.AddWithValue("@Name", toDo.Name);
                command.Parameters.AddWithValue("@Date", toDo.PlannedDate);

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

        public List<ToDo> GetAllToDos(string userId)
        {
            List<ToDo> results = new();
            using (sqlConnection = new(_connectionString))
            {
                string query = "SELECT [Id], [Name], [Description], [Date], [User_Id], [Status] FROM [ToDo] WHERE [User_id] = @userid ORDER BY [Date] desc";
                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@userid", userId);

                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ToDo result = new()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"]),
                                Description = Convert.ToString(reader["Description"]),
                                PlannedDate = Convert.ToDateTime(reader["Date"]),
                                UserId = Convert.ToString(reader["User_Id"]),
                                Status = Convert.ToInt32(reader["Status"])
                            };
                            results.Add(result);
                        }
                    }
                }
                catch
                {

                }
            }
            sqlConnection.Close();
            return results;
        }

        public List<ToDo> GetToDoByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public ToDo GetToDoById(int id)
        {
            ToDo result = new();
            using (sqlConnection = new(_connectionString))
            {
                string query = "SELECT [Id], [Name], [Description], [Date], [User_Id], [Status] FROM [ToDo] WHERE [Id] = @id";
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
                                Name = Convert.ToString(reader["Name"]),
                                Description = Convert.ToString(reader["Description"]),
                                PlannedDate = Convert.ToDateTime(reader["Date"]),
                                UserId = Convert.ToString(reader["User_Id"]),
                                Status = Convert.ToInt32(reader["Status"])
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

        public List<ToDo> GetToDoByName(string name, string userId)
        {
            List<ToDo> results = new();
            using (sqlConnection = new(_connectionString))
            {
                string query = "SELECT [Id], [Name], [Description], [Date], [User_Id], [Status] FROM [ToDo] WHERE [Name] = @name AND [User_id] = @userid ORDER BY [Date] desc";
                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@userid", userId);

                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ToDo result = new()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"]),
                                Description = Convert.ToString(reader["Description"]),
                                PlannedDate = Convert.ToDateTime(reader["Date"]),
                                UserId = Convert.ToString(reader["User_Id"]),
                                Status = Convert.ToInt32(reader["Status"])
                            };
                            results.Add(result);
                        }
                    }
                }
                catch
                {

                }
            }
            sqlConnection.Close();
            return results;
        }

        public ToDo GetToDoByNameOnDate(string name, string userId, DateTime ondate)
        {
            ToDo result = new();
            using (sqlConnection = new(_connectionString))
            {
                string query = "SELECT [Id], [Name], [Description], [Date], [User_Id], [Status] FROM [ToDo] WHERE [Name] = @name AND [User_id] = @userid AND [Date] = @date";
                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@userid", userId);

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
                                Name = Convert.ToString(reader["Name"]),
                                Description = Convert.ToString(reader["Description"]),
                                PlannedDate = Convert.ToDateTime(reader["Date"]),
                                UserId = Convert.ToString(reader["User_Id"]),
                                Status = Convert.ToInt32(reader["Status"])
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

        public bool UpdateStatus(int id, int status)
        {
            using (sqlConnection = new(_connectionString))
            {
                string query = "UPDATE [ToDo] SET [Status] = @status WHERE [Id] = @id";

                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@id", id); // Assuming there's an 'Id' property in ToDo

                try
                {
                    sqlConnection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0; // Success if one or more rows were updated
                }
                catch (Exception ex)
                {
                    // Log the exception, don't re-throw it
                    Console.WriteLine(ex.Message);
                    return false; // Return false to indicate an error
                }
            }
        }

        public bool UpdateToDo(ToDo toDo)
        {
            using (sqlConnection = new(_connectionString))
            {
                string query = "UPDATE [ToDo] SET [Name] = @name, [Description] = @description, [Date] = @date, [Status] = @status WHERE [Id] = @id AND [User_Id] = @userId";

                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@name", toDo.Name);
                command.Parameters.AddWithValue("@description", toDo.Description);
                command.Parameters.AddWithValue("@date", toDo.PlannedDate);
                command.Parameters.AddWithValue("@status", toDo.Status);
                command.Parameters.AddWithValue("@id", toDo.Id); // Assuming there's an 'Id' property in ToDo
                command.Parameters.AddWithValue("@userId", toDo.UserId);

                try
                {
                    sqlConnection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0; // Success if one or more rows were updated
                }
                catch (Exception ex)
                {
                    // Log the exception, don't re-throw it
                    Console.WriteLine(ex.Message);
                    return false; // Return false to indicate an error
                }
            }
        }
    }
}
