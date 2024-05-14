using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FitTracker.Models
{ 
    internal class DayRecordRepository
    {
        private string connectionString;

        public DayRecordRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddDayRecord(DayRecord record, int UserID)
        {
            int logId = 0;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"
            INSERT INTO DailyLogs (UserID, Date, Weight_kg, Calories, WaterIntake_liters, SleepHours, Notes)
            VALUES (@UserID, @Date, @Weight_kg, @Calories, @WaterIntake_liters, @SleepHours, @Notes);";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);  
                    cmd.Parameters.AddWithValue("@Date", record.Date);
                    cmd.Parameters.AddWithValue("@Weight_kg", record.Weight);
                    cmd.Parameters.AddWithValue("@Calories", record.Calories);
                    cmd.Parameters.AddWithValue("@WaterIntake_liters", record.WaterIntake);
                    cmd.Parameters.AddWithValue("@SleepHours", record.SleepHours);
                    cmd.Parameters.AddWithValue("@Notes", record.Notes);

                    cmd.ExecuteNonQuery();
                    record.LogID = (int)cmd.LastInsertedId;
                }

                // Insert activities
                InsertActivities(conn, record.LogID, record.Activities.ToList()); 
                // Insert moods
                InsertMoods(conn, record.LogID, record.Moods.ToList());
                // Insert other factors
                InsertOtherFactors(conn, record.LogID, record.OtherFactors.ToList());
            }
        }

        private void InsertActivities(MySqlConnection conn, int logId, IEnumerable<string> activities)
        {
            foreach (var activity in activities)
            {
                string sql = "INSERT INTO UserActivities (LogID, ActivityID) SELECT @LogID, ActivityID FROM Activities WHERE ActivityName = @ActivityName;";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@LogID", logId);
                    cmd.Parameters.AddWithValue("@ActivityName", activity);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertMoods(MySqlConnection conn, int logId, IEnumerable<string> moods)
        {
            foreach (var mood in moods)
            {
                string sql = "INSERT INTO UserMoods (LogID, MoodID) SELECT @LogID, MoodID FROM Moods WHERE MoodDescription = @MoodDescription;";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@LogID", logId);
                    cmd.Parameters.AddWithValue("@MoodDescription", mood);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertOtherFactors(MySqlConnection conn, int logId, IEnumerable<string> otherFactors)
        {
            foreach (var factor in otherFactors)
            {
                string sql = "INSERT INTO UserEvents (LogID, EventID) SELECT @LogID, EventID FROM Events WHERE EventName = @EventName;";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@LogID", logId);
                    cmd.Parameters.AddWithValue("@EventName", factor);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public async Task<List<DayRecord>> GetDayRecordsForUserAsync(int userId)
        {
            List<DayRecord> records = new List<DayRecord>();
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new MySqlCommand("SELECT * FROM DailyLogs WHERE UserID = @UserId ORDER BY Date DESC", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var record = new DayRecord
                        {
                            LogID = reader.IsDBNull(reader.GetOrdinal("LogID")) ? 0 : reader.GetInt32(reader.GetOrdinal("LogID")),
                            Date = reader.IsDBNull(reader.GetOrdinal("Date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Date")),
                            Weight = reader.IsDBNull(reader.GetOrdinal("Weight_kg")) ? (float?)null : reader.GetFloat(reader.GetOrdinal("Weight_kg")),
                            Calories = reader.IsDBNull(reader.GetOrdinal("Calories")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Calories")),
                            WaterIntake = reader.IsDBNull(reader.GetOrdinal("WaterIntake_liters")) ? (float?)null : reader.GetFloat(reader.GetOrdinal("WaterIntake_liters")),
                            SleepHours = reader.IsDBNull(reader.GetOrdinal("SleepHours")) ? (float?)null : reader.GetFloat(reader.GetOrdinal("SleepHours")),
                            Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? string.Empty : reader.GetString(reader.GetOrdinal("Notes")),
                        };
                        records.Add(record);
                    }
                }
            }
            
            return records;
        }




        public void DeleteDayRecord(int logId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Delete associated activities, moods, and other factors first
                DeleteActivities(conn, logId);
                DeleteMoods(conn, logId);
                DeleteOtherFactors(conn, logId);

                // Then delete the day record itself
                string sql = @"DELETE FROM DailyLogs WHERE LogID = @LogID";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@LogID", logId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DeleteActivities(MySqlConnection conn, int logId)
        {
            string sql = "DELETE FROM UserActivities WHERE LogID = @LogID";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@LogID", logId);
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteMoods(MySqlConnection conn, int logId)
        {
            string sql = "DELETE FROM UserMoods WHERE LogID = @LogID";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@LogID", logId);
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteOtherFactors(MySqlConnection conn, int logId)
        {
            string sql = "DELETE FROM UserEvents WHERE LogID = @LogID";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@LogID", logId);
                cmd.ExecuteNonQuery();
            }
        }



    }
}
