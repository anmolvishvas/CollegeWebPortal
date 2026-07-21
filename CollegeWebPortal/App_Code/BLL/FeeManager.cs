using System.Data;
using System.Data.SqlClient;
using CollegeWebPortal.DAL;

namespace CollegeWebPortal.BLL
{
    public static class FeeManager
    {
        public static DataTable GetAll()
        {
            return DBHelper.ExecuteDataTable(
                "SELECT f.FeeID, f.StudentID, s.RollNo, " +
                "(s.FirstName + ' ' + s.LastName) AS StudentName, " +
                "f.Amount, f.[Status], f.PaymentDate " +
                "FROM dbo.Fees f INNER JOIN dbo.Students s ON s.StudentID = f.StudentID " +
                "ORDER BY s.RollNo");
        }

        public static DataTable GetByStudent(int studentId)
        {
            return DBHelper.ExecuteDataTable(
                "SELECT f.FeeID, f.StudentID, s.RollNo, " +
                "(s.FirstName + ' ' + s.LastName) AS StudentName, " +
                "f.Amount, f.[Status], f.PaymentDate " +
                "FROM dbo.Fees f INNER JOIN dbo.Students s ON s.StudentID = f.StudentID " +
                "WHERE f.StudentID = @Id ORDER BY f.FeeID",
                false, new SqlParameter("@Id", studentId));
        }
    }
}
