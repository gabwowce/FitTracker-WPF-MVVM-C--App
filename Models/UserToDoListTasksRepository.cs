using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Models
{
    internal class UserToDoListTasksRepository
    {
        private string connectionString;

        public UserToDoListTasksRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddTask(UserToDoListTasks task, int UserID)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO ToDoTasks (UserID, Task, isCompleted) VALUES (@UserID, @Task, @isCompleted);";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Task", task.Task);
                    cmd.Parameters.AddWithValue("@isCompleted", task.isCompleted);

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public async Task<List<UserToDoListTasks>> GetToDoListTasksAsync(int userId)
        {
            List<UserToDoListTasks> tasks = new List<UserToDoListTasks>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string sql = @"SELECT * FROM ToDoTasks WHERE UserID = @UserId ORDER BY TaskID DESC";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var task = new UserToDoListTasks
                            {
                                TaskID = reader.GetInt32(reader.GetOrdinal("TaskID")),
                                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                Task = reader.GetString(reader.GetOrdinal("Task")),
                                isCompleted = reader.GetBoolean(reader.GetOrdinal("isCompleted"))
                            };
                            tasks.Add(task);
                        }
                    }
                }
            }
            return tasks;
        }

        public void DeleteToDoTask(int taskID)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"DELETE FROM ToDoTasks WHERE TaskID = @TaskID";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TaskID", taskID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

