using System.Data;
using UserRating.Infrastructure.Connection;
using UserRating.Infrastructure.RepositoryInterfaces;
using UserRating.Models;
using UserRating.Enums;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

namespace UserRating.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ConnectionSettings _connectionSettings;

        public UserRepository(ConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();

            using (var connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                var query =
                    "SELECT * FROM Users";

                var command = new SqlCommand(query, connection);

                command.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            users.Add(CheckUserIsFull(reader));
                        }
                    }
                }
            }

            return users;
        }

        public User Get(int id)
        {
            using (var connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                var query =
                    "SELECT * FROM Users WHERE Id = @Id";

                var command = new SqlCommand(query, connection);

                command.Parameters.Add(new SqlParameter("Id", SqlDbType.Int)
                {
                    Value = id
                });

                command.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            return CheckUserIsFull(reader);
                        }
                    }
                }

                return new User();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(User user)
        {
            using (var connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                var query =
                    "UPDATE Users SET " +
                    "FirstName = @FirstName, " +
                    "LastName = @LastName, " +
                    "Age = @Age, " +
                    "City = @City, " +
                    "AboutMe = @AboutMe, " +
                    "Avatar = @Avatar, " +
                    "Email = @Email, " +
                    "Login = @Login, " +
                    "Password = @Password, " +
                    "Role = @Role " +
                    "WHERE Id = @Id";

                var command = new SqlCommand(query, connection);

                command.Parameters.Add(new SqlParameter("Id", SqlDbType.Int)
                {
                    Value = user.Id
                });

                command.Parameters.AddWithValue("FirstName", user.FirstName);

                command.Parameters.AddWithValue("LastName", user.LastName);

                command.Parameters.Add(new SqlParameter("Age", SqlDbType.Int)
                {
                    Value = user.Age
                });

                command.Parameters.AddWithValue("City", user.City);

                command.Parameters.AddWithValue("AboutMe", user.AboutMe);

                command.Parameters.Add(new SqlParameter("Avatar", SqlDbType.Binary)
                {
                    Value = user.Avatar
                });

                command.Parameters.AddWithValue("Email", user.Email);

                command.Parameters.AddWithValue("Login", user.Login);

                command.Parameters.AddWithValue("Password", user.Password);

                command.Parameters.Add(new SqlParameter("Role", SqlDbType.Int)
                {
                    Value = user.Role
                });

                command.Connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public byte[] ConvertAvatarToByteArray(HttpRequest files)
        {
            byte[] avatarByteArray = Array.Empty<byte>();

            foreach (var file in files.Form.Files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);

                    avatarByteArray = memoryStream.ToArray();
                }
            }

            return avatarByteArray;
        }

        public void RemoveAvatar(int id)
        {
            using (var connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                var query =
                    "UPDATE Users SET Avatar = NULL WHERE Id = @Id";

                var command = new SqlCommand(query, connection);

                command.Parameters.Add(new SqlParameter("Id", SqlDbType.Int)
                {
                    Value = id
                });

                command.Connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public void Create(User user)
        {
            using (var connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                var query =
                    "INSERT INTO Users (Email, Login, Password, Role) VALUES (@Email, @Login, @Password, @Role)";

                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("Email", user.Email);

                command.Parameters.AddWithValue("Login", user.Login);

                command.Parameters.AddWithValue("Password", user.Password);

                command.Parameters.Add(new SqlParameter("Role", SqlDbType.Int)
                {
                    Value = user.Role
                });

                command.Connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public int GetLastId()
        {
            using (var connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                var query =
                    "SELECT MAX(Id) FROM Users";

                var command = new SqlCommand(query, connection);

                command.Connection.Open();

                return (int)command.ExecuteScalar();
            }
        }

        private User CheckUserIsFull(SqlDataReader reader)
        {
            return new User
            {
                Id = reader["Id"] == DBNull.Value ? default : Convert.ToInt32(reader["Id"]),
                FirstName = reader["FirstName"] == DBNull.Value ? default : reader["FirstName"].ToString(),
                LastName = reader["LastName"] == DBNull.Value ? default : reader["LastName"].ToString(),
                Age = reader["Age"] == DBNull.Value ? default : Convert.ToInt32(reader["Age"]),
                City = reader["City"] == DBNull.Value ? default : reader["City"].ToString(),
                AboutMe = reader["AboutMe"] == DBNull.Value ? default : reader["AboutMe"].ToString(),
                Likes = reader["Likes"] == DBNull.Value ? default : Convert.ToInt32(reader["Likes"]),
                Dislikes = reader["Dislikes"] == DBNull.Value ? default : Convert.ToInt32(reader["Dislikes"]),
                Avatar = reader["Avatar"] == DBNull.Value ? default : (byte[])reader["Avatar"],
                Email = reader["Email"] == DBNull.Value ? default : reader["Email"].ToString(),
                Login = reader["Login"] == DBNull.Value ? default : reader["Login"].ToString(),
                Password = reader["Password"] == DBNull.Value ? default : reader["Password"].ToString(),
                Role = reader["Role"] == DBNull.Value ? default : (Role)Convert.ToInt32(reader["Role"])
            };
        }
    }
}