using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CollegeWebPortal.DAL;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.BLL
{
    /// <summary>
    /// Student business logic. CRUD operations use STORED PROCEDURES
    /// (InsertStudent / UpdateStudent / DeleteStudent / SearchStudent).
    /// Searching and sorting are performed with LINQ.
    /// </summary>
    public static class StudentManager
    {
        /// <summary>Maps a DataRow (from SearchStudent) to a Student model.</summary>
        private static Student Map(DataRow r)
        {
            return new Student
            {
                StudentID = (int)r["StudentID"],
                RollNo = r["RollNo"].ToString(),
                FirstName = r["FirstName"].ToString(),
                LastName = r["LastName"].ToString(),
                Email = r["Email"] == System.DBNull.Value ? "" : r["Email"].ToString(),
                Phone = r["Phone"] == System.DBNull.Value ? "" : r["Phone"].ToString(),
                DepartmentID = r["DepartmentID"] == System.DBNull.Value ? 0 : (int)r["DepartmentID"],
                DepartmentName = r.Table.Columns.Contains("DepartmentName") && r["DepartmentName"] != System.DBNull.Value
                                 ? r["DepartmentName"].ToString() : "",
                Semester = r["Semester"] == System.DBNull.Value ? 0 : (int)r["Semester"],
                Address = r["Address"] == System.DBNull.Value ? "" : r["Address"].ToString()
            };
        }

        /// <summary>Returns all students as a list (via SearchStudent SP).</summary>
        public static List<Student> GetAll()
        {
            DataTable dt = DBHelper.ExecuteDataTable("dbo.SearchStudent", true,
                new SqlParameter("@Keyword", System.DBNull.Value));
            return dt.AsEnumerable().Select(Map).ToList();
        }

        /// <summary>
        /// LINQ powered search + sort. Data is fetched via the SearchStudent SP,
        /// then filtered/sorted in memory with LINQ (per project requirements).
        /// </summary>
        public static List<Student> Search(string keyword, string sortBy = "RollNo", bool ascending = true)
        {
            SqlParameter kw = new SqlParameter("@Keyword",
                string.IsNullOrWhiteSpace(keyword) ? (object)System.DBNull.Value : keyword);

            DataTable dt = DBHelper.ExecuteDataTable("dbo.SearchStudent", true, kw);
            IEnumerable<Student> query = dt.AsEnumerable().Select(Map);

            // LINQ filtering (extra client-side narrowing / case-insensitive)
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string k = keyword.Trim().ToLower();
                query = query.Where(s =>
                    s.RollNo.ToLower().Contains(k) ||
                    s.FullName.ToLower().Contains(k) ||
                    (s.Email ?? "").ToLower().Contains(k));
            }

            // LINQ sorting
            switch (sortBy)
            {
                case "Name":
                    query = ascending ? query.OrderBy(s => s.FirstName) : query.OrderByDescending(s => s.FirstName);
                    break;
                case "Semester":
                    query = ascending ? query.OrderBy(s => s.Semester) : query.OrderByDescending(s => s.Semester);
                    break;
                default:
                    query = ascending ? query.OrderBy(s => s.RollNo) : query.OrderByDescending(s => s.RollNo);
                    break;
            }

            return query.ToList();
        }

        public static Student GetById(int studentId)
        {
            DataTable dt = DBHelper.ExecuteDataTable(
                "SELECT s.StudentID, s.RollNo, s.FirstName, s.LastName, s.Email, s.Phone, " +
                "s.DepartmentID, d.DepartmentName, s.Semester, s.Address " +
                "FROM dbo.Students s LEFT JOIN dbo.Departments d ON d.DepartmentID = s.DepartmentID " +
                "WHERE s.StudentID = @Id",
                false, new SqlParameter("@Id", studentId));

            return dt.Rows.Count > 0 ? Map(dt.Rows[0]) : null;
        }

        /// <summary>Inserts a student using the InsertStudent stored procedure.</summary>
        public static int Insert(Student s)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@RollNo", s.RollNo),
                new SqlParameter("@FirstName", s.FirstName),
                new SqlParameter("@LastName", s.LastName),
                new SqlParameter("@Email", (object)s.Email ?? System.DBNull.Value),
                new SqlParameter("@Phone", (object)s.Phone ?? System.DBNull.Value),
                new SqlParameter("@DepartmentID", s.DepartmentID),
                new SqlParameter("@Semester", s.Semester),
                new SqlParameter("@Address", (object)s.Address ?? System.DBNull.Value)
            };
            object id = DBHelper.ExecuteScalar("dbo.InsertStudent", true, p);
            return id != null ? System.Convert.ToInt32(id) : 0;
        }

        /// <summary>Updates a student using the UpdateStudent stored procedure.</summary>
        public static void Update(Student s)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@StudentID", s.StudentID),
                new SqlParameter("@RollNo", s.RollNo),
                new SqlParameter("@FirstName", s.FirstName),
                new SqlParameter("@LastName", s.LastName),
                new SqlParameter("@Email", (object)s.Email ?? System.DBNull.Value),
                new SqlParameter("@Phone", (object)s.Phone ?? System.DBNull.Value),
                new SqlParameter("@DepartmentID", s.DepartmentID),
                new SqlParameter("@Semester", s.Semester),
                new SqlParameter("@Address", (object)s.Address ?? System.DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.UpdateStudent", true, p);
        }

        /// <summary>Deletes a student using the DeleteStudent stored procedure.</summary>
        public static void Delete(int studentId)
        {
            DBHelper.ExecuteNonQuery("dbo.DeleteStudent", true,
                new SqlParameter("@StudentID", studentId));
        }
    }
}
