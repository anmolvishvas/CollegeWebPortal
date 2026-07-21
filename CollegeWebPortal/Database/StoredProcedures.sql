/**********************************************************************************
  College Web Portal - Stored Procedures
  Run this AFTER CollegePortalDB.sql
**********************************************************************************/
USE CollegePortalDB;
GO

------------------------------------------------------------------------------
-- InsertStudent
------------------------------------------------------------------------------
IF OBJECT_ID('dbo.InsertStudent','P') IS NOT NULL DROP PROCEDURE dbo.InsertStudent;
GO
CREATE PROCEDURE dbo.InsertStudent
    @RollNo       NVARCHAR(20),
    @FirstName    NVARCHAR(50),
    @LastName     NVARCHAR(50),
    @Email        NVARCHAR(100),
    @Phone        NVARCHAR(20),
    @DepartmentID INT,
    @Semester     INT,
    @Address      NVARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Students (RollNo, FirstName, LastName, Email, Phone, DepartmentID, Semester, Address)
    VALUES (@RollNo, @FirstName, @LastName, @Email, @Phone, @DepartmentID, @Semester, @Address);

    SELECT SCOPE_IDENTITY() AS NewStudentID;
END
GO

------------------------------------------------------------------------------
-- UpdateStudent
------------------------------------------------------------------------------
IF OBJECT_ID('dbo.UpdateStudent','P') IS NOT NULL DROP PROCEDURE dbo.UpdateStudent;
GO
CREATE PROCEDURE dbo.UpdateStudent
    @StudentID    INT,
    @RollNo       NVARCHAR(20),
    @FirstName    NVARCHAR(50),
    @LastName     NVARCHAR(50),
    @Email        NVARCHAR(100),
    @Phone        NVARCHAR(20),
    @DepartmentID INT,
    @Semester     INT,
    @Address      NVARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Students
       SET RollNo       = @RollNo,
           FirstName    = @FirstName,
           LastName     = @LastName,
           Email        = @Email,
           Phone        = @Phone,
           DepartmentID = @DepartmentID,
           Semester     = @Semester,
           Address      = @Address
     WHERE StudentID    = @StudentID;
END
GO

------------------------------------------------------------------------------
-- DeleteStudent
------------------------------------------------------------------------------
IF OBJECT_ID('dbo.DeleteStudent','P') IS NOT NULL DROP PROCEDURE dbo.DeleteStudent;
GO
CREATE PROCEDURE dbo.DeleteStudent
    @StudentID INT
AS
BEGIN
    SET NOCOUNT ON;
    -- Remove dependent rows first to satisfy foreign keys
    DELETE FROM dbo.Fees        WHERE StudentID = @StudentID;
    DELETE FROM dbo.Marks       WHERE StudentID = @StudentID;
    DELETE FROM dbo.Attendance  WHERE StudentID = @StudentID;
    DELETE FROM dbo.Users       WHERE Role = 'Student' AND RefID = @StudentID;
    DELETE FROM dbo.Students    WHERE StudentID = @StudentID;
END
GO

------------------------------------------------------------------------------
-- SearchStudent  (by name / roll no / email; empty = all)
------------------------------------------------------------------------------
IF OBJECT_ID('dbo.SearchStudent','P') IS NOT NULL DROP PROCEDURE dbo.SearchStudent;
GO
CREATE PROCEDURE dbo.SearchStudent
    @Keyword NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT s.StudentID, s.RollNo, s.FirstName, s.LastName, s.Email, s.Phone,
           s.DepartmentID, d.DepartmentName, s.Semester, s.Address
      FROM dbo.Students s
      LEFT JOIN dbo.Departments d ON d.DepartmentID = s.DepartmentID
     WHERE @Keyword IS NULL OR @Keyword = ''
        OR s.RollNo    LIKE '%' + @Keyword + '%'
        OR s.FirstName LIKE '%' + @Keyword + '%'
        OR s.LastName  LIKE '%' + @Keyword + '%'
        OR s.Email     LIKE '%' + @Keyword + '%'
     ORDER BY s.RollNo;
END
GO

------------------------------------------------------------------------------
-- InsertFaculty
------------------------------------------------------------------------------
IF OBJECT_ID('dbo.InsertFaculty','P') IS NOT NULL DROP PROCEDURE dbo.InsertFaculty;
GO
CREATE PROCEDURE dbo.InsertFaculty
    @FirstName    NVARCHAR(50),
    @LastName     NVARCHAR(50),
    @DepartmentID INT,
    @Email        NVARCHAR(100),
    @Phone        NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Faculty (FirstName, LastName, DepartmentID, Email, Phone)
    VALUES (@FirstName, @LastName, @DepartmentID, @Email, @Phone);

    SELECT SCOPE_IDENTITY() AS NewFacultyID;
END
GO

------------------------------------------------------------------------------
-- InsertAttendance
------------------------------------------------------------------------------
IF OBJECT_ID('dbo.InsertAttendance','P') IS NOT NULL DROP PROCEDURE dbo.InsertAttendance;
GO
CREATE PROCEDURE dbo.InsertAttendance
    @StudentID INT,
    @SubjectID INT,
    @FacultyID INT,
    @Date      DATE,
    @Status    NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Attendance (StudentID, SubjectID, FacultyID, [Date], [Status])
    VALUES (@StudentID, @SubjectID, @FacultyID, @Date, @Status);
END
GO

------------------------------------------------------------------------------
-- InsertMarks  (updates if the student/subject row already exists)
------------------------------------------------------------------------------
IF OBJECT_ID('dbo.InsertMarks','P') IS NOT NULL DROP PROCEDURE dbo.InsertMarks;
GO
CREATE PROCEDURE dbo.InsertMarks
    @StudentID     INT,
    @SubjectID     INT,
    @InternalMarks INT,
    @ExternalMarks INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.Marks WHERE StudentID = @StudentID AND SubjectID = @SubjectID)
        UPDATE dbo.Marks
           SET InternalMarks = @InternalMarks,
               ExternalMarks = @ExternalMarks
         WHERE StudentID = @StudentID AND SubjectID = @SubjectID;
    ELSE
        INSERT INTO dbo.Marks (StudentID, SubjectID, InternalMarks, ExternalMarks)
        VALUES (@StudentID, @SubjectID, @InternalMarks, @ExternalMarks);
END
GO

------------------------------------------------------------------------------
-- InsertNotice
------------------------------------------------------------------------------
IF OBJECT_ID('dbo.InsertNotice','P') IS NOT NULL DROP PROCEDURE dbo.InsertNotice;
GO
CREATE PROCEDURE dbo.InsertNotice
    @Title       NVARCHAR(150),
    @Description NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Notices (Title, Description, PostedDate)
    VALUES (@Title, @Description, GETDATE());
END
GO

PRINT 'Stored procedures created successfully.';
GO
