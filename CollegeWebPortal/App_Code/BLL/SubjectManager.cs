using System.Data;
using System.Data.SqlClient;
using CollegeWebPortal.DAL;

namespace CollegeWebPortal.BLL
{
    public static class SubjectManager
    {
        public static DataTable GetAll()
        {
            return DBHelper.ExecuteDataTable(
                "SELECT s.SubjectID, s.SubjectName, s.CourseID, c.CourseName " +
                "FROM dbo.Subjects s LEFT JOIN dbo.Courses c ON c.CourseID = s.CourseID " +
                "ORDER BY s.SubjectName");
        }

        /// <summary>Subjects for the drop-down lists (id + name only).</summary>
        public static DataTable GetForDropDown()
        {
            return DBHelper.ExecuteDataTable(
                "SELECT SubjectID, SubjectName FROM dbo.Subjects ORDER BY SubjectName");
        }

        public static void Insert(string name, int courseId)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@Course", courseId)
            };
            DBHelper.ExecuteNonQuery(
                "INSERT INTO dbo.Subjects (SubjectName, CourseID) VALUES (@Name,@Course)", false, p);
        }

        public static void Update(int id, string name, int courseId)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Name", name),
                new SqlParameter("@Course", courseId)
            };
            DBHelper.ExecuteNonQuery(
                "UPDATE dbo.Subjects SET SubjectName=@Name, CourseID=@Course WHERE SubjectID=@Id", false, p);
        }

        public static void Delete(int id)
        {
            DBHelper.ExecuteNonQuery("DELETE FROM dbo.Subjects WHERE SubjectID=@Id",
                false, new SqlParameter("@Id", id));
        }
    }
}
