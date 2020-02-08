using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_AlumniDirectory : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Checks valid login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 1 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            lblMsg.Text = "";
            if (!IsPostBack)
            {
                gvAlumniDirectory.DataSource = "";
                gvAlumniDirectory.DataBind();

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("--Select--");

                var courses = (from c in ue.Courses
                               where c.cvalid == true
                               select c).ToList();

                if (courses.Count != 0)
                {
                    foreach (var data in courses)
                        ddlCourse.Items.Add(data.cname);
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }

    }
    /// <summary>
    /// Gets detail of alumni and transfers user to edit alumni page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAlumniDirectory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string aid = e.CommandArgument.ToString();
            Session["alumniid"] = aid;

            Response.Redirect("EditAlumni.aspx");
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Gets list of alumni in that course on Submit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCourse.SelectedIndex != 0)
            {
                var alumniList = (from u in ue.Alumni
                                  join c in ue.Courses
                                  on u.Courses.cid equals c.cid
                                  where u.Roles.rid == 4 && c.cname == ddlCourse.Text
                                  select new { AlumniID = u.aid, Name = u.aname, Department = u.Courses.cname, PassoutYear = u.apassyear, Email = u.aemail, Company = u.acompany, Valid = u.avalid }).ToList();

                if (alumniList.Count != 0)
                {
                    gvAlumniDirectory.DataSource = alumniList;
                    gvAlumniDirectory.DataBind();
                }
                else
                {
                    gvAlumniDirectory.DataSource = "";
                    gvAlumniDirectory.DataBind();
                    lblMsg.Text = "No alumni for this course!";
                }
            }
            else
            {
                gvAlumniDirectory.DataSource = "";
                gvAlumniDirectory.DataBind();
                lblMsg.Text = "No course selected!";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
