using System.Data;
using System.Data.SqlClient;
using CollegeWebPortal.DAL;

namespace CollegeWebPortal.BLL
{
    public static class CourseManager
    {
        public static DataTable GetAll()
        {
            return DBHelper.ExecuteDataTable(
                "SELECT c.CourseID, c.CourseName, c.DepartmentID, d.DepartmentName " +
                "FROM dbo.Courses c LEFT JOIN dbo.Departments d ON d.DepartmentID = c.DepartmentID " +
                "ORDER BY c.CourseName");
        }

        public static void Insert(string name, int departmentId)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@Dept", departmentId)
            };
            DBHelper.ExecuteNonQuery(
                "INSERT INTO dbo.Courses (CourseName, DepartmentID) VALUES (@Name,@Dept)", false, p);
        }

        public static void Update(int id, string name, int departmentId)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Name", name),
                new SqlParameter("@Dept", departmentId)
            };
            DBHelper.ExecuteNonQuery(
                "UPDATE dbo.Courses SET CourseName=@Name, DepartmentID=@Dept WHERE CourseID=@Id", false, p);
        }

        public static void Delete(int id)
        {
            DBHelper.ExecuteNonQuery("DELETE FROM dbo.Courses WHERE CourseID=@Id",
                false, new SqlParameter("@Id", id));
        }
    }
}
