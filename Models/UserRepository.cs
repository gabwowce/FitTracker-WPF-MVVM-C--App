using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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

        public void UpdateUser(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"
                    UPDATE Users SET Username = @Username, Gender = @Gender, Weight_kg = @Height_cm,
                    Weight_kg = @Weight_kg, DateOfBirth = @DateOfBirth WHERE UserId = @UserId";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", user.UserId);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Gender", user.Gender);
                    cmd.Parameters.AddWithValue("@Height_cm", user.Height);
                    cmd.Parameters.AddWithValue("@Weight_kg", user.Weight);
                    cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public async Task<User> GetUserProfileAsync(int userId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string sql = "SELECT * FROM Users WHERE UserID = @UserID";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                UserId = userId,
                                Username = reader["Username"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Weight = Convert.ToSingle(reader["Weight_kg"]),
                                Height = Convert.ToInt32(reader["Height_cm"]),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"])
                            };
                        }
                    }
                }
            }

            return null;
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



        public int CheckCredentials(string username, string password)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                // Tikriname, ar egzistuoja vartotojas su tokiu vartotojo vardu
                var cmd = new MySqlCommand("SELECT UserID, PasswordHash FROM Users WHERE Username = @Username", conn);
                cmd.Parameters.AddWithValue("@Username", username);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int userId = Convert.ToInt32(reader["UserID"]);

                        // Patikriname slaptažodį
                        if (CorrectPassword(username, password))
                        {
                            return userId;  // Grąžiname vartotojo ID, jei slaptažodis teisingas
                        }
                    }
                }
            }
            return 0;  // Grąžiname 0, jei vartotojas nerastas arba slaptažodis neteisingas
        }
    }
}

