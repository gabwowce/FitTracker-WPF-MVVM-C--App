-- Users table
CREATE TABLE Users (
    UserID INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(255) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Gender VARCHAR(50),
    Height_cm INT,
    Weight_kg FLOAT,
    DateOfBirth DATE,
    RegistrationDate DATE NOT NULL
);

-- Daily logs table
CREATE TABLE DailyLogs (
    LogID INT PRIMARY KEY AUTO_INCREMENT,
    UserID INT,
    Date DATE,
    Weight_kg FLOAT,
    Calories INT,
    WaterIntake_liters FLOAT,
    SleepHours FLOAT,
    Notes TEXT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Activities table
CREATE TABLE Activities (
    ActivityID INT PRIMARY KEY AUTO_INCREMENT,
    ActivityName VARCHAR(255)
);

-- User activities table
CREATE TABLE UserActivities (
    UserActivityID INT PRIMARY KEY AUTO_INCREMENT,
    LogID INT,
    ActivityID INT,
    FOREIGN KEY (LogID) REFERENCES DailyLogs(LogID),
    FOREIGN KEY (ActivityID) REFERENCES Activities(ActivityID)
);

-- Moods table
CREATE TABLE Moods (
    MoodID INT PRIMARY KEY AUTO_INCREMENT,
    MoodDescription VARCHAR(255)
);

-- User moods table
CREATE TABLE UserMoods (
    UserMoodID INT PRIMARY KEY AUTO_INCREMENT,
    LogID INT,
    MoodID INT,
    FOREIGN KEY (LogID) REFERENCES DailyLogs(LogID),
    FOREIGN KEY (MoodID) REFERENCES Moods(MoodID)
);

-- Events table
CREATE TABLE Events (
    EventID INT PRIMARY KEY AUTO_INCREMENT,
    EventName VARCHAR(255)
);

-- User events table
CREATE TABLE UserEvents (
    UserEventID INT PRIMARY KEY AUTO_INCREMENT,
    LogID INT,
    EventID INT,
    FOREIGN KEY (LogID) REFERENCES DailyLogs(LogID),
    FOREIGN KEY (EventID) REFERENCES Events(EventID)
);








INSERT INTO Activities (ActivityName) VALUES
('Yoga'),
('Running'),
('Gym'),
('Swimming'),
('Walking'),
('Team Sports'),
('Cycling'),
('Dancing'),
('Balance');

INSERT INTO Moods (MoodDescription) VALUES
('Calm'),
('Happy'),
('Energetic'),
('Frisky'),
('Sad'),
('Depressed'),
('Confused'),
('Irritated'),
('Low Energy');


INSERT INTO Events (EventName) VALUES
('Travel'),
('Stress'),
('Meditation'),
('Disease or Injury'),
('Alcohol'),
('Social Activity'),
('Cooking'),
('Workload'),
('Healthy Eating'),
('Sun Exposure'),
('Reading'),
('Relaxation'),
('Creative Activities'),
('Pet Care'),
('Learning New Skills');

