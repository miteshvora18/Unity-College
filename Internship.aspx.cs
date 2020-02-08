using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class Internship : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    /// <summary>
    /// Gets upcoming available internship on Load..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                var getJobs = (from pl in ue.PlacementsList
                               join pt in ue.PlacementType
                               on pl.PlacementType.ptid equals pt.ptid
                               join c in ue.Courses
                               on pl.Courses.cid equals c.cid
                               where pl.pvalid == true && pt.ptid == 2 && pl.pdate > DateTime.Now
                               orderby pl.pdate ascending
                               select new { Company = pl.pcompanyname, Profile = pl.pjobtitle, PlacementDate = pl.pdate, Location = pl.pinterviewlocation, EligibleCourse = c.cname, Eligibility = pl.peligibility, Website = pl.pwebsite }).ToList();
                //if (getJobs != null)
                //{
                //    foreach (var data in getJobs)
                //    {
                //        if (data.pl.pwebsite != null)
                //        {
                //            lblJobsDetail.Text += "<div class='eventheader'>" + data.pl.pcompanyname + "-" + data.pl.pjobtitle + "</div><div style='font-size:large;'>Date and Time:" + data.pl.pdate.ToString("MMM. dd yyyy HH:mm") + "<br/>For Department:" + data.c.cname + "<br/>Location:" + data.pl.pinterviewlocation + "<br/>Eligibility:" + data.pl.peligibility + "<br/>";
                //            lblJobsDetail.Text += "Website:" + data.pl.pwebsite + "</div><br/>";
                //        }
                //        else
                //            lblJobsDetail.Text += "<div class='eventheader'>" + data.pl.pcompanyname + "-" + data.pl.pjobtitle + "</div><div style='font-size:large;'>Date and Time:" + data.pl.pdate.ToString("MMM. dd yyyy HH:mm") + "<br/>For Course:" + data.c.cname + "<br/>Location:" + data.pl.pinterviewlocation + "<br/>Eligibility:" + data.pl.peligibility + "<br/>";
                //    }

                //}

                if (getJobs.Count != 0)
                {
                    var courseList = getJobs.Select(u => new { Company = u.Company, Profile = u.Profile, PlacementDate = u.PlacementDate.ToString("MMM. dd yyyy HH:mm"), Location = u.Location, EligibleCourse = u.EligibleCourse, Eligibility = u.Eligibility, Website = u.Website });

                    gvJobs.DataSource = courseList;
                    gvJobs.DataBind();
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
