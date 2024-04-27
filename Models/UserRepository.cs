using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Models
{
    internal class UserRepository
    {
        private string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"
                    INSERT INTO Users (Username, PasswordHash, Gender, Height_cm, Weight_kg, DateOfBirth, RegistrationDate)
                    VALUES (@Username, @PasswordHash, @Gender, @Height_cm, @Weight_kg, @DateOfBirth, @RegistrationDate)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@Gender", user.Gender);
                    cmd.Parameters.AddWithValue("@Height_cm", user.Height);
                    cmd.Parameters.AddWithValue("@Weight_kg", user.Weight);
                    cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                    cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool UserExists(string username)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public bool CorrectPassword(string username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT PasswordHash FROM Users WHERE Username = @Username";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string storedPasswordHash = Convert.ToString(result);
                        string inputPasswordHash = HashHelper.HashPassword(password);
                        return storedPasswordHash == inputPasswordHash;
                    }
                    return false;
                }
            }
        }
    }
}

