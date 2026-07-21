using System;

namespace CollegeWebPortal.Models
{
    /// <summary>Login account.</summary>
    public class UserAccount
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public int RefID { get; set; }
    }

    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }

    public class Faculty
    {
        public int FacultyID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string FullName { get { return (FirstName + " " + LastName).Trim(); } }
    }

    public class Student
    {
        public int StudentID { get; set; }
        public string RollNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int Semester { get; set; }
        public string Address { get; set; }

        public string FullName { get { return (FirstName + " " + LastName).Trim(); } }
    }

    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }

    public class Subject
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }
    }

    public class AttendanceRecord
    {
        public int AttendanceID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string RollNo { get; set; }
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int FacultyID { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }

    public class Mark
    {
        public int MarkID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string RollNo { get; set; }
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int InternalMarks { get; set; }
        public int ExternalMarks { get; set; }
        public int Total { get; set; }
    }

    public class Notice
    {
        public int NoticeID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }
    }

    public class Fee
    {
        public int FeeID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string RollNo { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
