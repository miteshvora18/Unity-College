using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class Courses : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    /// <summary>
    /// Gets list of courses in college..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            gvCourse.DataSource = "";
            gvCourse.DataBind();

            if (!IsPostBack)
            {
                var getCourse = (from c in ue.Courses
                                 where c.cvalid == true
                                 orderby c.cstartyear
                                 select new { CourseName = c.cname, Duration_Years = c.cduration, Description = c.cdescription, TotalSemester = c.csemesters, StartYear = c.cstartyear, Accreditation = c.caccreditation }).ToList();
                //if (getcourse != null)
                //{
                //    foreach (var data in getcourse)
                //    {
                //        if (data.caccreditation != null)
                //        {
                //        lblCourseData.Text += "<div class='eventheader'>" + data.cname + "</div><div style='font-size:large;'>Start Year:" + data.cstartyear.Year + "<br/>Duration:" + data.cduration + " Years<br/>Semesters:" + data.csemesters + "<br/>Description:" + data.cdescription + "<br/>";
                //        lblCourseData.Text += "Accreditation:" + data.caccreditation+"</div><br/>";
                //        }
                //        else
                //            lblCourseData.Text += "<div class='eventheader'>" + data.cname + "</div><div style='font-size:large;'>Start Year:" + data.cstartyear.Year + "<br/>Duration:" + data.cduration + " Years<br/>Semesters:" + data.csemesters + "<br/>Description:" + data.cdescription + "</div><br/>";


                //    }
                //}

                //Gets course list in grid view..
                if (getCourse.Count != 0)
                {
                    var courselist = getCourse.Select(u => new { CourseName = u.CourseName, Duration_Years = u.Duration_Years, Description = u.Description, TotalSemester = u.TotalSemester, StartYear = u.StartYear.ToString("yyyy"), Accreditation = u.Accreditation });
                    gvCourse.DataSource = courselist;
                    gvCourse.DataBind();
                }
            }
        }
        catch (Exception e1)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Error" + e1.Message + "');", true);
        }
    }

}
