using System.Data;
using System.Data.SqlClient;
using CollegeWebPortal.DAL;

namespace CollegeWebPortal.BLL
{
    /// <summary>Marks business logic. Upload uses the InsertMarks stored procedure.</summary>
    public static class MarksManager
    {
        public static DataTable GetAll()
        {
            return DBHelper.ExecuteDataTable(
                "SELECT m.MarkID, m.StudentID, s.RollNo, " +
                "(s.FirstName + ' ' + s.LastName) AS StudentName, " +
                "m.SubjectID, sub.SubjectName, m.InternalMarks, m.ExternalMarks, m.Total " +
                "FROM dbo.Marks m " +
                "INNER JOIN dbo.Students s ON s.StudentID = m.StudentID " +
                "INNER JOIN dbo.Subjects sub ON sub.SubjectID = m.SubjectID " +
                "ORDER BY s.RollNo");
        }

        public static DataTable GetByStudent(int studentId)
        {
            return DBHelper.ExecuteDataTable(
                "SELECT m.MarkID, m.StudentID, s.RollNo, " +
                "(s.FirstName + ' ' + s.LastName) AS StudentName, " +
                "m.SubjectID, sub.SubjectName, m.InternalMarks, m.ExternalMarks, m.Total " +
                "FROM dbo.Marks m " +
                "INNER JOIN dbo.Students s ON s.StudentID = m.StudentID " +
                "INNER JOIN dbo.Subjects sub ON sub.SubjectID = m.SubjectID " +
                "WHERE m.StudentID = @Id ORDER BY sub.SubjectName",
                false, new SqlParameter("@Id", studentId));
        }

        /// <summary>Inserts/updates marks via the InsertMarks stored procedure.</summary>
        public static void Upload(int studentId, int subjectId, int internalMarks, int externalMarks)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@StudentID", studentId),
                new SqlParameter("@SubjectID", subjectId),
                new SqlParameter("@InternalMarks", internalMarks),
                new SqlParameter("@ExternalMarks", externalMarks)
            };
            DBHelper.ExecuteNonQuery("dbo.InsertMarks", true, p);
        }
    }
}
