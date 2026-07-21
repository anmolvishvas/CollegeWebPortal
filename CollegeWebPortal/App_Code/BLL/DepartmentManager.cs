using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CollegeWebPortal.DAL;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.BLL
{
    public static class DepartmentManager
    {
        public static DataTable GetAll()
        {
            return DBHelper.ExecuteDataTable(
                "SELECT DepartmentID, DepartmentName FROM dbo.Departments ORDER BY DepartmentName");
        }

        public static List<Department> GetAllList()
        {
            List<Department> list = new List<Department>();
            DataTable dt = GetAll();
            foreach (DataRow r in dt.Rows)
            {
                list.Add(new Department
                {
                    DepartmentID = (int)r["DepartmentID"],
                    DepartmentName = r["DepartmentName"].ToString()
                });
            }
            return list;
        }

        public static void Insert(string name)
        {
            SqlParameter[] p = { new SqlParameter("@Name", name) };
            DBHelper.ExecuteNonQuery(
                "INSERT INTO dbo.Departments (DepartmentName) VALUES (@Name)", false, p);
        }

        public static void Update(int id, string name)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Name", name)
            };
            DBHelper.ExecuteNonQuery(
                "UPDATE dbo.Departments SET DepartmentName=@Name WHERE DepartmentID=@Id", false, p);
        }

        public static void Delete(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            DBHelper.ExecuteNonQuery(
                "DELETE FROM dbo.Departments WHERE DepartmentID=@Id", false, p);
        }
    }
}
