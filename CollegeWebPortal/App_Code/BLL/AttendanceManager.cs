using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CollegeWebPortal.DAL;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.BLL
{
    /// <summary>
    /// Attendance business logic. Marking uses the InsertAttendance stored proc.
    /// Filtering of records is done with LINQ.
    /// </summary>
    public static class AttendanceManager
    {
        private static AttendanceRecord Map(DataRow r)
        {
            return new AttendanceRecord
            {
                AttendanceID = (int)r["AttendanceID"],
                StudentID = (int)r["StudentID"],
                RollNo = r["RollNo"].ToString(),
                StudentName = r["StudentName"].ToString(),
                SubjectID = (int)r["SubjectID"],
                SubjectName = r["SubjectName"].ToString(),
                Date = Convert.ToDateTime(r["Date"]),
                Status = r["Status"].ToString()
            };
        }

        private static DataTable Raw()
        {
            return DBHelper.ExecuteDataTable(
                "SELECT a.AttendanceID, a.StudentID, s.RollNo, " +
                "(s.FirstName + ' ' + s.LastName) AS StudentName, " +
                "a.SubjectID, sub.SubjectName, a.[Date], a.[Status] " +
                "FROM dbo.Attendance a " +
                "INNER JOIN dbo.Students s ON s.StudentID = a.StudentID " +
                "INNER JOIN dbo.Subjects sub ON sub.SubjectID = a.SubjectID " +
                "ORDER BY a.[Date] DESC");
        }

        public static List<AttendanceRecord> GetAll()
        {
            return Raw().AsEnumerable().Select(Map).ToList();
        }

        /// <summary>LINQ filtering of attendance by student and/or subject.</summary>
        public static List<AttendanceRecord> Filter(int studentId, int subjectId, string status)
        {
            IEnumerable<AttendanceRecord> query = Raw().AsEnumerable().Select(Map);

            if (studentId > 0) query = query.Where(a => a.StudentID == studentId);
            if (subjectId > 0) query = query.Where(a => a.SubjectID == subjectId);
            if (!string.IsNullOrEmpty(status)) query = query.Where(a => a.Status == status);

            return query.OrderByDescending(a => a.Date).ToList();
        }

        public static List<AttendanceRecord> GetByStudent(int studentId)
        {
            return Filter(studentId, 0, null);
        }

        /// <summary>Marks attendance via the InsertAttendance stored procedure.</summary>
        public static void Mark(int studentId, int subjectId, int facultyId, DateTime date, string status)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@StudentID", studentId),
                new SqlParameter("@SubjectID", subjectId),
                new SqlParameter("@FacultyID", facultyId),
                new SqlParameter("@Date", date.Date),
                new SqlParameter("@Status", status)
            };
            DBHelper.ExecuteNonQuery("dbo.InsertAttendance", true, p);
        }

        /// <summary>Attendance percentage for a student (used on the dashboard).</summary>
        public static double GetPercentage(int studentId)
        {
            List<AttendanceRecord> list = GetByStudent(studentId);
            if (list.Count == 0) return 0;
            int present = list.Count(a => a.Status == "Present");
            return Math.Round(present * 100.0 / list.Count, 1);
        }
    }
}
