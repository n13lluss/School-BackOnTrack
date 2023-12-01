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
            _connectionString = configuration.GetConnectionString("SchoolConnection");
            sqlConnection = new SqlConnection(_connectionString);
        }
        public bool CreateToDo(ToDo toDo)
        {
            using (sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                SqlTransaction transaction = sqlConnection.BeginTransaction();

                try
                {
                    string insertToDoQuery = @"INSERT INTO [ToDo] ([Name], [Description], [Date], [Status]) 
                                       VALUES (@name, @description, @date, @status);
                                       SELECT SCOPE_IDENTITY();";

                    SqlCommand command = new(insertToDoQuery, sqlConnection, transaction);
                    command.Parameters.AddWithValue("@name", toDo.Name);
                    command.Parameters.AddWithValue("@description", toDo.Description);
                    command.Parameters.AddWithValue("@date", toDo.PlannedDate);
                    command.Parameters.AddWithValue("@status", toDo.Status);

                    // Execute the first query to insert into ToDo and retrieve the generated ID
                    int todoId = Convert.ToInt32(command.ExecuteScalar());

                    // Insert into UserToDo using the generated ToDo ID
                    string insertUserToDoQuery = @"INSERT INTO UserActivity (UserId, ActivityId) 
                                           VALUES (@userId, @todoId);";

                    command = new(insertUserToDoQuery, sqlConnection, transaction);
                    command.Parameters.AddWithValue("@userId", toDo.UserId);
                    command.Parameters.AddWithValue("@todoId", todoId);

                    // Execute the second query to insert into UserToDo
                    command.ExecuteNonQuery();

                    // If both queries are successful, commit the transaction
                    transaction.Commit();

                    return true; // Success
                }
                catch (Exception ex)
                {
                    // Log the exception, and roll back the transaction on failure
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    return false; // Return false to indicate an error
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }


        public bool DeleteToDo(ToDo toDo)
        {
            using (sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                SqlTransaction transaction = sqlConnection.BeginTransaction();

                try
                {
                    string deleteUserToDoQuery = @"DELETE FROM [UserActivity] WHERE [ActivityId] = @id";
                    

                    SqlCommand command = new(deleteUserToDoQuery, sqlConnection, transaction);
                    command.Parameters.AddWithValue("@id", toDo.Id);

                    // Execute the first query to insert into ToDo and retrieve the generated ID
                    int todoId = Convert.ToInt32(command.ExecuteScalar());

                    // Insert into UserToDo using the generated ToDo ID
                    string deleteToDoQuery = @"DELETE FROM [ToDo] WHERE [Id] = @id";

                    command = new(deleteToDoQuery, sqlConnection, transaction);
                    command.Parameters.AddWithValue("@id", toDo.Id);

                    // Execute the second query to insert into UserToDo
                    command.ExecuteNonQuery();

                    // If both queries are successful, commit the transaction
                    transaction.Commit();

                    return true; // Success
                }
                catch (Exception ex)
                {
                    // Log the exception, and roll back the transaction on failure
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    return false; // Return false to indicate an error
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        public List<ToDo> GetAllToDos(string userId)
        {
            List<ToDo> results = new();
            using (sqlConnection = new(_connectionString))
            {
                string query = @"
    SELECT TD.[Id], TD.Name, TD.Description, TD.Status, TD.Date
    FROM ToDo as TD
    JOIN UserActivity as UA ON TD.Id = UA.ActivityId
    WHERE UA.UserId = @userid
    ORDER BY TD.Date DESC";
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

        public List<ToDo> GetToDoByDate(DateTime date, string userId)
        {
            List<ToDo> todoList = new List<ToDo>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT TD.[Id], td.Name, td.Description, td.Status, td.Date 
FROM ToDo as TD 
JOIN UserActivity as UA 
    ON TD.Id = UA.ActivityId 
WHERE UA.UserId = @userid AND TD.[Date] = @date
Order by td.Date Desc";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@userId", userId);

                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ToDo todo = new ToDo
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"]),
                                Description = Convert.ToString(reader["Description"]),
                                PlannedDate = Convert.ToDateTime(reader["Date"]),
                                Status = Convert.ToInt32(reader["Status"])
                            };
                            todoList.Add(todo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle or log the exception as needed.
                    Console.WriteLine(ex.Message);
                }
            }

            return todoList;
        }


        public ToDo GetToDoById(int id)
        {
            ToDo result = new();
            using (sqlConnection = new(_connectionString))
            {
                string query = @"SELECT TD.[Id], td.Name, td.Description, td.Status, td.Date 
FROM ToDo as TD 
JOIN UserActivity as UA 
    ON TD.Id = UA.ActivityId 
WHERE TD.[Id] = @id
Order by td.Date Desc";
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
                string query = @"SELECT TD.[Id], td.Name, td.Description, td.Status, td.Date 
FROM[BackOnTrack].[dbo].ToDo as TD 
JOIN[BackOnTrack].[dbo].UserActivity as UA 
    ON TD.Id = UA.ActivityId 
WHERE UA.UserId = @userid AND TD.[Name] = @name
Order by td.Date Desc";
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
                string query = @"SELECT TD.[Id], TD.Name, TD.Description, TD.Status, TD.Date 
FROM ToDo as TD 
JOIN UserActivity as UA 
    ON TD.Id = UA.ActivityId 
WHERE UA.UserId = @userid AND TD.[Name] = @name AND TD.[Date] = @date
Order by td.Date Desc";
                SqlCommand command = new(query, sqlConnection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@userid", userId);
                command.Parameters.AddWithValue("@date", ondate);

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
