using System;
using System.Data;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Helpers;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.StudentArea
{
    public partial class Dashboard : BasePage
    {
        public Dashboard() { RequiredRole = "Student"; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int sid = SessionHelper.RefId;

                    Student s = StudentManager.GetById(sid);
                    lblName.Text = s != null ? s.FullName : SessionHelper.Username;
                    lblRoll.Text = s != null ? s.RollNo : string.Empty;

                    lblAttendance.Text = AttendanceManager.GetPercentage(sid).ToString();
                    lblSubjects.Text = MarksManager.GetByStudent(sid).Rows.Count.ToString();

                    DataTable fees = FeeManager.GetByStudent(sid);
                    lblFee.Text = ComputeFeeStatus(fees);

                    gvNotices.DataSource = NoticeManager.GetAll();
                    gvNotices.DataBind();
                }
                catch (Exception) { }
            }
        }

        private string ComputeFeeStatus(DataTable fees)
        {
            foreach (DataRow r in fees.Rows)
            {
                if (string.Equals(r["Status"].ToString(), "Pending", StringComparison.OrdinalIgnoreCase))
                    return "Pending";
            }
            return fees.Rows.Count == 0 ? "N/A" : "Paid";
        }
    }
}
