using System.Data.SqlClient;
using System.Configuration;

namespace Data_Access_Layer
{
    public class AuthDAL
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString;

        public bool UserExists(string username, string email)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(
                "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email",
                connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                connection.Open();
                return (int)command.ExecuteScalar() > 0;
            }
        }


        public int CreateUser(string username, string email, string passwordHash)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(@"INSERT INTO Users (Username, Email, Passwordhash, Currency) OUTPUT INSERTED.UserId VALUES (@Username, @Email, @PasswordHash, 200)", connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public AuthUser GetUser(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(@"SELECT UserId, Username, Passwordhash FROM Users WHERE Username = @Username", connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    return new AuthUser
                    {
                        UserId = (int)reader["UserId"],
                        Username = (string)reader["Username"],
                        PasswordHash = (string)reader["PasswordHash"]
                    };

                }

            }
        }
    }

    public class AuthUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
