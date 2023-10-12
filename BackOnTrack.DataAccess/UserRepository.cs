using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BackOnTrack.DataAccess
{
    //TODO dubbele interface implementatie Interface sergegation
    public class UserRepository : IUserRepository
    {
        private readonly string? _connectionString;
        private readonly IConfiguration _configuration;
        private SqlConnection sqlConnection;

        public UserRepository()
        { 
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            this._connectionString = _configuration.GetConnectionString("DefaultConnection");
            sqlConnection = new SqlConnection(_connectionString);
        }

        public List<UserModel> GetAll()
        {
            List<UserModel> Users = new List<UserModel>();

            try

            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [User]", sqlConnection))
                    {
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserModel user = new UserModel
                                {   
                                    Id = reader.GetInt32("id"),
                                    Username = reader["Username"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString()
                                };
                                Users.Add(user);
                            }
                        }
                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting users", ex);
            }

            return Users;
        }

        public UserModel GetById(int id)
        {
            UserModel user = null;

            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [User] WHERE Id = @Id", sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new UserModel
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Username = reader["Username"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the user by Id", ex);
            }

            return user;
        }

        bool IUserRepository.CreateUser(UserModel user)
        {
            using (sqlConnection)
            {
                string query = "INSERT INTO [User] ([Username], [Email], [Password], [FirstName], [Lastname], [IdString]) VALUES (@username, @email, @password, @firstname, @lastname, @idstring)";

                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@firstname", user.FirstName);
                command.Parameters.AddWithValue("@lastname", user.LastName);

                try
                {
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception("An error occurred while creating the user.", ex);
                }
            }
            return true;
        }
    }
}
