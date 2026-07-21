/**********************************************************************************
  College Web Portal - Database Schema + Sample Data
  Database : CollegePortalDB
  Engine   : Microsoft SQL Server
  ------------------------------------------------------------------------------
  HOW TO RUN
    1. Open this file in SQL Server Management Studio (SSMS).
    2. Execute the whole script (F5). It creates the database, tables and data.
    3. Then run StoredProcedures.sql to create the stored procedures.
  ------------------------------------------------------------------------------
  PASSWORD HASHING
    Passwords are stored as an UPPERCASE hex SHA-256 hash of  (password|salt).
    The SAME formula is implemented in C# (App_Code/Helpers/SecurityHelper.cs)
    so logins validate correctly. Default salt: 'C0lleg3P0rt@l$alt'
  ------------------------------------------------------------------------------
  DEFAULT LOGINS (username / password)
    admin    / Admin@123     (Admin)
    jsmith   / Faculty@123   (Faculty)
    rkumar   / Faculty@123   (Faculty)
    CS2101   / Student@123   (Student)  -> Roll No login
    CS2102   / Student@123   (Student)
    EC2101   / Student@123   (Student)
**********************************************************************************/

------------------------------------------------------------------------------
-- 1. CREATE DATABASE
------------------------------------------------------------------------------
IF DB_ID('CollegePortalDB') IS NULL
BEGIN
    CREATE DATABASE CollegePortalDB;
END
GO

USE CollegePortalDB;
GO

------------------------------------------------------------------------------
-- 2. DROP EXISTING TABLES (safe re-run) - child tables first
------------------------------------------------------------------------------
IF OBJECT_ID('dbo.Fees','U')        IS NOT NULL DROP TABLE dbo.Fees;
IF OBJECT_ID('dbo.Marks','U')       IS NOT NULL DROP TABLE dbo.Marks;
IF OBJECT_ID('dbo.Attendance','U')  IS NOT NULL DROP TABLE dbo.Attendance;
IF OBJECT_ID('dbo.Notices','U')     IS NOT NULL DROP TABLE dbo.Notices;
IF OBJECT_ID('dbo.Subjects','U')    IS NOT NULL DROP TABLE dbo.Subjects;
IF OBJECT_ID('dbo.Courses','U')     IS NOT NULL DROP TABLE dbo.Courses;
IF OBJECT_ID('dbo.Students','U')    IS NOT NULL DROP TABLE dbo.Students;
IF OBJECT_ID('dbo.Faculty','U')     IS NOT NULL DROP TABLE dbo.Faculty;
IF OBJECT_ID('dbo.Departments','U') IS NOT NULL DROP TABLE dbo.Departments;
IF OBJECT_ID('dbo.Users','U')       IS NOT NULL DROP TABLE dbo.Users;
GO

------------------------------------------------------------------------------
-- 3. CREATE TABLES
------------------------------------------------------------------------------

-- Users -----------------------------------------------------------------
CREATE TABLE dbo.Users
(
    UserID    INT IDENTITY(1,1) PRIMARY KEY,
    Username  NVARCHAR(50)  NOT NULL UNIQUE,
    Password  NVARCHAR(200) NOT NULL,           -- SHA-256 hash (hex)
    Role      NVARCHAR(20)  NOT NULL,           -- Admin | Faculty | Student
    RefID     INT NULL,                         -- links to StudentID / FacultyID
    CreatedOn DATETIME NOT NULL DEFAULT(GETDATE())
);
GO

-- Departments -----------------------------------------------------------
CREATE TABLE dbo.Departments
(
    DepartmentID   INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL
);
GO

-- Faculty ---------------------------------------------------------------
CREATE TABLE dbo.Faculty
(
    FacultyID    INT IDENTITY(1,1) PRIMARY KEY,
    FirstName    NVARCHAR(50)  NOT NULL,
    LastName     NVARCHAR(50)  NOT NULL,
    DepartmentID INT NULL REFERENCES dbo.Departments(DepartmentID),
    Email        NVARCHAR(100) NULL,
    Phone        NVARCHAR(20)  NULL
);
GO

-- Students --------------------------------------------------------------
CREATE TABLE dbo.Students
(
    StudentID    INT IDENTITY(1,1) PRIMARY KEY,
    RollNo       NVARCHAR(20)  NOT NULL UNIQUE,
    FirstName    NVARCHAR(50)  NOT NULL,
    LastName     NVARCHAR(50)  NOT NULL,
    Email        NVARCHAR(100) NULL,
    Phone        NVARCHAR(20)  NULL,
    DepartmentID INT NULL REFERENCES dbo.Departments(DepartmentID),
    Semester     INT NULL,
    Address      NVARCHAR(250) NULL
);
GO

-- Courses ---------------------------------------------------------------
CREATE TABLE dbo.Courses
(
    CourseID     INT IDENTITY(1,1) PRIMARY KEY,
    CourseName   NVARCHAR(100) NOT NULL,
    DepartmentID INT NULL REFERENCES dbo.Departments(DepartmentID)
);
GO

-- Subjects --------------------------------------------------------------
CREATE TABLE dbo.Subjects
(
    SubjectID   INT IDENTITY(1,1) PRIMARY KEY,
    SubjectName NVARCHAR(100) NOT NULL,
    CourseID    INT NULL REFERENCES dbo.Courses(CourseID)
);
GO

-- Attendance ------------------------------------------------------------
CREATE TABLE dbo.Attendance
(
    AttendanceID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID    INT NOT NULL REFERENCES dbo.Students(StudentID),
    SubjectID    INT NOT NULL REFERENCES dbo.Subjects(SubjectID),
    FacultyID    INT NULL REFERENCES dbo.Faculty(FacultyID),
    [Date]       DATE NOT NULL,
    [Status]     NVARCHAR(10) NOT NULL          -- Present | Absent
);
GO

-- Marks -----------------------------------------------------------------
CREATE TABLE dbo.Marks
(
    MarkID        INT IDENTITY(1,1) PRIMARY KEY,
    StudentID     INT NOT NULL REFERENCES dbo.Students(StudentID),
    SubjectID     INT NOT NULL REFERENCES dbo.Subjects(SubjectID),
    InternalMarks INT NOT NULL DEFAULT(0),
    ExternalMarks INT NOT NULL DEFAULT(0),
    Total         AS (InternalMarks + ExternalMarks) PERSISTED
);
GO

-- Notices ---------------------------------------------------------------
CREATE TABLE dbo.Notices
(
    NoticeID    INT IDENTITY(1,1) PRIMARY KEY,
    Title       NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    PostedDate  DATETIME NOT NULL DEFAULT(GETDATE())
);
GO

-- Fees ------------------------------------------------------------------
CREATE TABLE dbo.Fees
(
    FeeID       INT IDENTITY(1,1) PRIMARY KEY,
    StudentID   INT NOT NULL REFERENCES dbo.Students(StudentID),
    Amount      DECIMAL(10,2) NOT NULL,
    [Status]    NVARCHAR(20) NOT NULL,          -- Paid | Pending
    PaymentDate DATETIME NULL
);
GO

------------------------------------------------------------------------------
-- 4. SAMPLE DATA
------------------------------------------------------------------------------

-- Helper: password hash expression must match SecurityHelper.cs
--   HashHex = CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CAST(pwd + '|' + salt AS VARCHAR(200))), 2)

-- Departments
INSERT INTO dbo.Departments (DepartmentName) VALUES
 (N'Computer Science & Engineering'),
 (N'Electronics & Communication'),
 (N'Mechanical Engineering'),
 (N'Civil Engineering'),
 (N'Information Technology');

-- Faculty
INSERT INTO dbo.Faculty (FirstName, LastName, DepartmentID, Email, Phone) VALUES
 (N'John',  N'Smith',  1, N'jsmith@college.edu',  N'9876500001'),
 (N'Rahul', N'Kumar',  2, N'rkumar@college.edu',  N'9876500002'),
 (N'Meera', N'Nair',   1, N'mnair@college.edu',   N'9876500003'),
 (N'Anil',  N'Verma',  3, N'averma@college.edu',  N'9876500004');

-- Students
INSERT INTO dbo.Students (RollNo, FirstName, LastName, Email, Phone, DepartmentID, Semester, Address) VALUES
 (N'CS2101', N'Aarav',  N'Sharma',  N'aarav@student.edu',  N'9000000001', 1, 3, N'12 MG Road, Pune'),
 (N'CS2102', N'Diya',   N'Patel',   N'diya@student.edu',   N'9000000002', 1, 3, N'45 Park Street, Mumbai'),
 (N'CS2103', N'Kabir',  N'Singh',   N'kabir@student.edu',  N'9000000003', 1, 5, N'9 Lake View, Nagpur'),
 (N'EC2101', N'Isha',   N'Reddy',   N'isha@student.edu',   N'9000000004', 2, 3, N'78 Hill Road, Hyderabad'),
 (N'EC2102', N'Vivaan', N'Gupta',   N'vivaan@student.edu', N'9000000005', 2, 5, N'21 Green Ave, Delhi'),
 (N'ME2101', N'Ananya', N'Iyer',    N'ananya@student.edu', N'9000000006', 3, 3, N'5 Sea View, Chennai');

-- Courses
INSERT INTO dbo.Courses (CourseName, DepartmentID) VALUES
 (N'B.Tech Computer Science', 1),
 (N'B.Tech Electronics',      2),
 (N'B.Tech Mechanical',       3),
 (N'B.Tech Information Tech',  5);

-- Subjects
INSERT INTO dbo.Subjects (SubjectName, CourseID) VALUES
 (N'Data Structures',            1),
 (N'Database Management Systems',1),
 (N'Operating Systems',          1),
 (N'Digital Electronics',        2),
 (N'Signals & Systems',          2),
 (N'Thermodynamics',             3);

-- Users (passwords hashed via HASHBYTES to match SecurityHelper.cs)
DECLARE @salt VARCHAR(50) = 'C0lleg3P0rt@l$alt';

INSERT INTO dbo.Users (Username, Password, Role, RefID) VALUES
 (N'admin',  CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CAST('Admin@123'   + '|' + @salt AS VARCHAR(200))), 2), N'Admin',   NULL),
 (N'jsmith', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CAST('Faculty@123' + '|' + @salt AS VARCHAR(200))), 2), N'Faculty', 1),
 (N'rkumar', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CAST('Faculty@123' + '|' + @salt AS VARCHAR(200))), 2), N'Faculty', 2),
 (N'CS2101', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CAST('Student@123' + '|' + @salt AS VARCHAR(200))), 2), N'Student', 1),
 (N'CS2102', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CAST('Student@123' + '|' + @salt AS VARCHAR(200))), 2), N'Student', 2),
 (N'EC2101', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CAST('Student@123' + '|' + @salt AS VARCHAR(200))), 2), N'Student', 4);

-- Attendance (StudentID, SubjectID, FacultyID, Date, Status)
INSERT INTO dbo.Attendance (StudentID, SubjectID, FacultyID, [Date], [Status]) VALUES
 (1, 1, 1, DATEADD(DAY,-5, CAST(GETDATE() AS DATE)), N'Present'),
 (1, 1, 1, DATEADD(DAY,-4, CAST(GETDATE() AS DATE)), N'Present'),
 (1, 2, 1, DATEADD(DAY,-3, CAST(GETDATE() AS DATE)), N'Absent'),
 (2, 1, 1, DATEADD(DAY,-5, CAST(GETDATE() AS DATE)), N'Present'),
 (2, 2, 1, DATEADD(DAY,-3, CAST(GETDATE() AS DATE)), N'Present'),
 (4, 4, 2, DATEADD(DAY,-2, CAST(GETDATE() AS DATE)), N'Present');

-- Marks (StudentID, SubjectID, Internal, External)
INSERT INTO dbo.Marks (StudentID, SubjectID, InternalMarks, ExternalMarks) VALUES
 (1, 1, 28, 62),
 (1, 2, 25, 55),
 (2, 1, 22, 48),
 (2, 2, 27, 60),
 (4, 4, 24, 51);

-- Notices
INSERT INTO dbo.Notices (Title, Description, PostedDate) VALUES
 (N'Semester Exams Schedule', N'Semester examinations begin from next month. Check the timetable on the notice board.', DATEADD(DAY,-2, GETDATE())),
 (N'Annual Sports Day',       N'Annual sports day will be held on the college ground. All students are invited.',        DATEADD(DAY,-1, GETDATE())),
 (N'Library Timings Updated', N'The central library will now remain open till 8 PM on all working days.',               GETDATE());

-- Fees
INSERT INTO dbo.Fees (StudentID, Amount, [Status], PaymentDate) VALUES
 (1, 45000.00, N'Paid',    DATEADD(DAY,-30, GETDATE())),
 (2, 45000.00, N'Pending', NULL),
 (3, 45000.00, N'Paid',    DATEADD(DAY,-20, GETDATE())),
 (4, 42000.00, N'Pending', NULL),
 (5, 42000.00, N'Paid',    DATEADD(DAY,-15, GETDATE())),
 (6, 40000.00, N'Pending', NULL);
GO

PRINT 'CollegePortalDB schema and sample data created successfully.';
PRINT 'Now run StoredProcedures.sql to create the stored procedures.';
GO
