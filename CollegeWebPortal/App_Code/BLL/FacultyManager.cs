using System.Data;
using System.Data.SqlClient;
using CollegeWebPortal.DAL;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.BLL
{
    public static class FacultyManager
    {
        public static DataTable GetAll()
        {
            return DBHelper.ExecuteDataTable(
                "SELECT f.FacultyID, f.FirstName, f.LastName, f.Email, f.Phone, " +
                "f.DepartmentID, d.DepartmentName " +
                "FROM dbo.Faculty f LEFT JOIN dbo.Departments d ON d.DepartmentID = f.DepartmentID " +
                "ORDER BY f.FirstName");
        }

        public static Faculty GetById(int facultyId)
        {
            DataTable dt = DBHelper.ExecuteDataTable(
                "SELECT f.FacultyID, f.FirstName, f.LastName, f.Email, f.Phone, " +
                "f.DepartmentID, d.DepartmentName " +
                "FROM dbo.Faculty f LEFT JOIN dbo.Departments d ON d.DepartmentID = f.DepartmentID " +
                "WHERE f.FacultyID = @Id", false, new SqlParameter("@Id", facultyId));

            if (dt.Rows.Count == 0) return null;
            DataRow r = dt.Rows[0];
            return new Faculty
            {
                FacultyID = (int)r["FacultyID"],
                FirstName = r["FirstName"].ToString(),
                LastName = r["LastName"].ToString(),
                Email = r["Email"] == System.DBNull.Value ? "" : r["Email"].ToString(),
                Phone = r["Phone"] == System.DBNull.Value ? "" : r["Phone"].ToString(),
                DepartmentID = r["DepartmentID"] == System.DBNull.Value ? 0 : (int)r["DepartmentID"],
                DepartmentName = r["DepartmentName"] == System.DBNull.Value ? "" : r["DepartmentName"].ToString()
            };
        }

        /// <summary>Inserts a faculty member using the InsertFaculty stored procedure.</summary>
        public static int Insert(Faculty f)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@FirstName", f.FirstName),
                new SqlParameter("@LastName", f.LastName),
                new SqlParameter("@DepartmentID", f.DepartmentID),
                new SqlParameter("@Email", (object)f.Email ?? System.DBNull.Value),
                new SqlParameter("@Phone", (object)f.Phone ?? System.DBNull.Value)
            };
            object id = DBHelper.ExecuteScalar("dbo.InsertFaculty", true, p);
            return id != null ? System.Convert.ToInt32(id) : 0;
        }

        public static void Update(Faculty f)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Id", f.FacultyID),
                new SqlParameter("@FirstName", f.FirstName),
                new SqlParameter("@LastName", f.LastName),
                new SqlParameter("@DepartmentID", f.DepartmentID),
                new SqlParameter("@Email", (object)f.Email ?? System.DBNull.Value),
                new SqlParameter("@Phone", (object)f.Phone ?? System.DBNull.Value)
            };
            DBHelper.ExecuteNonQuery(
                "UPDATE dbo.Faculty SET FirstName=@FirstName, LastName=@LastName, " +
                "DepartmentID=@DepartmentID, Email=@Email, Phone=@Phone WHERE FacultyID=@Id",
                false, p);
        }

        public static void Delete(int facultyId)
        {
            DBHelper.ExecuteNonQuery("DELETE FROM dbo.Faculty WHERE FacultyID=@Id",
                false, new SqlParameter("@Id", facultyId));
        }
    }
}
