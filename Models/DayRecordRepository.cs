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

        public void AddDayRecord(DayRecord record)
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
                    cmd.Parameters.AddWithValue("@UserID", 1);  
                    cmd.Parameters.AddWithValue("@Date", record.Date);
                    cmd.Parameters.AddWithValue("@Weight_kg", record.Weight);
                    cmd.Parameters.AddWithValue("@Calories", record.Calories);
                    cmd.Parameters.AddWithValue("@WaterIntake_liters", record.WaterIntake);
                    cmd.Parameters.AddWithValue("@SleepHours", record.SleepHours);
                    cmd.Parameters.AddWithValue("@Notes", record.Notes);

                    cmd.ExecuteNonQuery();
                    logId = (int)cmd.LastInsertedId;
                }

                // Insert activities
                InsertActivities(conn, logId, record.Activities.ToList()); ;
                // Insert moods
                InsertMoods(conn, logId, record.Moods.ToList());
                // Insert other factors
                InsertOtherFactors(conn, logId, record.OtherFactors.ToList());
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

    }
}
