using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CollegeWebPortal.DAL;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.BLL
{
    /// <summary>Notice board business logic. Posting uses the InsertNotice stored proc.</summary>
    public static class NoticeManager
    {
        private static Notice Map(DataRow r)
        {
            return new Notice
            {
                NoticeID = (int)r["NoticeID"],
                Title = r["Title"].ToString(),
                Description = r["Description"] == DBNull.Value ? "" : r["Description"].ToString(),
                PostedDate = Convert.ToDateTime(r["PostedDate"])
            };
        }

        private static DataTable Raw()
        {
            return DBHelper.ExecuteDataTable(
                "SELECT NoticeID, Title, Description, PostedDate FROM dbo.Notices ORDER BY PostedDate DESC");
        }

        public static List<Notice> GetAll()
        {
            return Raw().AsEnumerable().Select(Map).ToList();
        }

        public static Notice GetById(int noticeId)
        {
            DataTable dt = DBHelper.ExecuteDataTable(
                "SELECT NoticeID, Title, Description, PostedDate FROM dbo.Notices WHERE NoticeID=@Id",
                false, new SqlParameter("@Id", noticeId));
            return dt.Rows.Count > 0 ? Map(dt.Rows[0]) : null;
        }

        /// <summary>LINQ powered notice search (by title/description).</summary>
        public static List<Notice> Search(string keyword)
        {
            IEnumerable<Notice> query = Raw().AsEnumerable().Select(Map);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string k = keyword.Trim().ToLower();
                query = query.Where(n =>
                    n.Title.ToLower().Contains(k) ||
                    (n.Description ?? "").ToLower().Contains(k));
            }
            return query.OrderByDescending(n => n.PostedDate).ToList();
        }

        /// <summary>Posts a notice via the InsertNotice stored procedure.</summary>
        public static void Insert(string title, string description)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Title", title),
                new SqlParameter("@Description", (object)description ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.InsertNotice", true, p);
        }

        public static void Delete(int id)
        {
            DBHelper.ExecuteNonQuery("DELETE FROM dbo.Notices WHERE NoticeID=@Id",
                false, new SqlParameter("@Id", id));
        }
    }
}
