using System;
using System.Collections.Generic;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Helpers;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.StudentArea
{
    public partial class ProfilePage : BasePage
    {
        public ProfilePage() { RequiredRole = "Student"; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Student s = StudentManager.GetById(SessionHelper.RefId);
                    if (s != null)
                    {
                        lblName.Text = s.FullName;
                        lblRoll.Text = s.RollNo;
                        dv.DataSource = new List<Student> { s };
                        dv.DataBind();
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
