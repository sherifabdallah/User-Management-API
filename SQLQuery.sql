USE DotNetCourseDatabase;
GO
/*

--------------- Create Tabels ---------------
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Gender VARCHAR(10),
    Active BIT NOT NULL
);

CREATE TABLE UserJobInfo (
    UserId INT PRIMARY KEY IDENTITY,
    JobTitle VARCHAR(100) NOT NULL,
    Department VARCHAR(100) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE UserSalary (
    UserId INT PRIMARY KEY IDENTITY,
    Salary DECIMAL(10, 2) NOT NULL,
    AvgSalary DECIMAL(10, 2) NOT NULL
);

---------------------------------------------


*/



SELECT * FROM Users;
SELECT * FROM UserJobInfo;
SELECT * FROM UserSalary;
