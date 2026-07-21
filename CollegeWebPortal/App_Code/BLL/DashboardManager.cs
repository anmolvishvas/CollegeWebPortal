using System;
using CollegeWebPortal.DAL;

namespace CollegeWebPortal.BLL
{
    /// <summary>Simple aggregate counts used on the dashboards.</summary>
    public static class DashboardManager
    {
        private static int Count(string table)
        {
            object o = DBHelper.ExecuteScalar("SELECT COUNT(*) FROM dbo." + table);
            return o == null ? 0 : Convert.ToInt32(o);
        }

        public static int Students { get { return Count("Students"); } }
        public static int Faculty { get { return Count("Faculty"); } }
        public static int Departments { get { return Count("Departments"); } }
        public static int Courses { get { return Count("Courses"); } }
        public static int Subjects { get { return Count("Subjects"); } }
        public static int Notices { get { return Count("Notices"); } }
    }
}
